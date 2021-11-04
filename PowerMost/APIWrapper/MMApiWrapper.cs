using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using RestSharp.Validation;
using System.IO;
using PowerMost.Model;

namespace PowerMost.APIWrapper
{
    public class MMApiWrapper
    {
        private Matterproxy proxy;

        public MMApiWrapper()
        {
            proxy = new Matterproxy(); 
        }

        public MMApiWrapper(string baseurl, string username, string password)
        {
            proxy = new Matterproxy(baseurl, username, password); 
        }

        public MMApiWrapper(string baseurl, string accesstoken)
        {
            proxy = new Matterproxy(baseurl, accesstoken);
        }

        private string _sanitizeName(string name)
        {
            name = name.ToLower();
            name = name.Replace(' ', '-');

            return name; 
        }

        public bool UserExists(string username)
        {
            var user = GetUserByUserName(username);
            return !String.IsNullOrWhiteSpace(user.email);
        }

        public bool ChannelExists(string channelname, string teamname)
        {
            Ensure.NotEmpty(channelname, "Channel name");
            Ensure.NotEmpty(teamname, "Team name");

            bool result = true;

            try
            {
                GetChannelByNameAndTeam(channelname, teamname);
            } catch 
            {
                result = false;
            }

            return result;


        }

        public List<MMChannel> GetChannels()
        {
            var request = new RestRequest("channels");
            return proxy.Execute<List<MMChannel>>(request);
        }

        public bool TeamExists(string teamname)
        {
            Ensure.NotEmpty(teamname, "Teamname");
            bool result = false;

            teamname = _sanitizeName(teamname); 

            var request = new RestRequest("teams/name/{name}/exists");
            request.AddParameter("name", teamname, ParameterType.UrlSegment);
            var o = proxy.Execute<Dictionary<string, bool>>(request);
            o.TryGetValue("exists", out result);

            return result;
        }

        public MMChannel GetChannelByNameAndTeam(string channelname, string teamname, bool includeDeleted = false)
        {
            Ensure.NotEmpty(channelname, "Channel name");
            Ensure.NotEmpty(teamname, "Team name");

            channelname = _sanitizeName(channelname);
            teamname = _sanitizeName(teamname); 

            var request = new RestRequest("teams/name/{team_name}/channels/name/{channel_name}");
            request.AddParameter("team_name", teamname, ParameterType.UrlSegment);
            request.AddParameter("channel_name", channelname, ParameterType.UrlSegment);

            if (includeDeleted)
            {
                request.AddParameter("include_deleted", "true", ParameterType.QueryString);
            }

            return proxy.Execute<MMChannel>(request);
        }

        public MMUser GetUserByUserName(string username)
        {
            Ensure.NotEmpty(username, "Username");
            
            username = _sanitizeName(username);
            
            var request = new RestRequest("users/username/{username}");
            request.AddParameter("username", username, ParameterType.UrlSegment);
            return proxy.Execute<MMUser>(request);
        }

        public MMTeam GetTeamByTeamName(string teamname)
        {
            Ensure.NotEmpty(teamname, "Teamname");

            teamname = _sanitizeName(teamname);

            var request = new RestRequest("teams/name/{name}");
            request.AddParameter("name", teamname, ParameterType.UrlSegment);
            return proxy.Execute<MMTeam>(request);
        }

        public void PostToAllTeams(string message, string channelname = "town-square")
        {

        }

        public MMPost PostToChannel(string message, string teamname, string channelname)
        {
            var channel = GetChannelByNameAndTeam(channelname, teamname);

            var request = new RestRequest("posts", Method.POST);
            request.AddJsonBody(new { channel_id = channel.id, message = message });

            return proxy.Execute<MMPost>(request); 
        }

        public void AddUserToChannel(string username, string teamname, string channelname)
        {
            var user = GetUserByUserName(username);
            var channel = GetChannelByNameAndTeam(channelname, teamname);

            var request = new RestRequest("channels/{channel_id}/members", Method.POST);
            request.AddParameter("channel_id", channel.id, ParameterType.UrlSegment);
            request.AddJsonBody(new { user_id = user.id });

            proxy.Execute<Object>(request);

        }

        public void RemoveUserFromChannel(string username, string teamname, string channelname)
        {
            var user = GetUserByUserName(username);
            var channel = GetChannelByNameAndTeam(channelname, teamname);

            var request = new RestRequest("channels/{channel_id}/members/{user_id}", Method.DELETE);
            request.AddParameter("channel_id", channel.id, ParameterType.UrlSegment);
            request.AddParameter("user_id", user.id, ParameterType.UrlSegment);

            proxy.Execute<Object>(request);

        }

        public void DeleteChannel(string teamname, string channelname)
        {
            var channel = GetChannelByNameAndTeam(channelname, teamname, true);

            if(channel.delete_at > 0)
            {
                return; 
            }

            var request = new RestRequest("channels/{channel_id}", Method.DELETE);
            request.AddParameter("channel_id", channel.id, ParameterType.UrlSegment);
            
            proxy.Execute<Object>(request);
        }

        public MMChannel RestoreChannel(string teamname, string channelname)
        {
            var channel = GetChannelByNameAndTeam(channelname, teamname, true);
            var request = new RestRequest("channels/{channel_id}/restore", Method.POST);
            request.AddParameter("channel_id", channel.id, ParameterType.UrlSegment);

            return proxy.Execute<MMChannel>(request);
        }

        public MMChannel CreateDirectChannel(string userA, string userB)
        {
            string userAid = GetUserByUserName(userA).id;
            string userBid = GetUserByUserName(userB).id; 

            var request = new RestRequest("channels/direct", Method.POST);
            request.AddJsonBody(new string[] { userAid, userBid });

            return proxy.Execute<MMChannel>(request);
        }

        public MMChannel CreateGroupChannel(string[] users)
        {
            string[] ids = new string[users.Length]; 
            List<MMUser> usrs = GetUsers(users); 
            for(int i = 0; i < users.Length; i++)
            {
                ids[i] = usrs[i].id;
            }

            var request = new RestRequest("channels/group", Method.POST);
            request.AddJsonBody(ids);

            return proxy.Execute<MMChannel>(request);

        }


        public void AddUserToTeam(string username, string teamname)
        {
            var user = GetUserByUserName(username);
            var team = GetTeamByTeamName(teamname);

            var request = new RestRequest("teams/{team_id}/members", Method.POST);
            request.AddParameter("team_id", team.id, ParameterType.UrlSegment);
            request.AddJsonBody(new { team_id = team.id, user_id = user.id });

            proxy.Execute<Object>(request);

        }

        public void RemoveUserFromTeam(string username, string teamname)
        {
            var user = GetUserByUserName(username);
            var team = GetTeamByTeamName(teamname);

            var request = new RestRequest("teams/{team_id}/members/{user_id}", Method.DELETE);
            request.AddParameter("team_id", team.id, ParameterType.UrlSegment);
            request.AddParameter("user_id", user.id, ParameterType.UrlSegment);

            proxy.Execute<Object>(request);
        }


        public void SetUserPhoto(string username,  string picturePath)
        {

            var user = GetUserByUserName(username); 
            var request = new RestRequest("users/{user_id}/image", Method.POST);

            request.AddFile("image", picturePath); 
            request.AddParameter("user_id", user.id, ParameterType.UrlSegment);
            proxy.Execute<Object>(request);
        }


        public MMUser GetUserByEMail(string email)
        {
            Ensure.NotEmpty(email, "Email");

            email = email.ToLower(); 

            var request = new RestRequest("users/email/{email}");
            request.AddParameter("email", email, ParameterType.UrlSegment);
            return proxy.Execute<MMUser>(request);
        }

        public List<MMUser> GetUsers(string[] usernames )
        {
            var request = new RestRequest("users/usernames", Method.POST);
            request.AddJsonBody(usernames);
            return proxy.Execute<List<MMUser>>(request);
        }

        public MMChannel CreatePublicChannel(string teamname, string channelname, string displayname)
        {
            return CreateChannel(teamname, channelname, displayname, MMChannel.ChannelType.O); 
        }

        public MMChannel CreatePrivateChannel(string teamname, string channelname, string displayname)
        {
            return CreateChannel(teamname, channelname, displayname, MMChannel.ChannelType.P);
        }

        private MMChannel CreateChannel(string teamname, string channelname, string displayname, MMChannel.ChannelType channelType)
        {

            string teamid = GetTeamByTeamName(teamname).id;
            channelname = _sanitizeName(channelname);

            if(ChannelExists(channelname, teamname))
            {
                var check = GetChannelByNameAndTeam(channelname, teamname, true);
                if(check.delete_at > 0)
                {
                    return RestoreChannel(teamname, channelname); 
                }
                else
                {
                    throw new Exception($"Channel {channelname} already exists");
                }
            }

            MMChannel channel = new MMChannel() { team_id = teamid, name = channelname, display_name = displayname, type = Enum.GetName(typeof(MMChannel.ChannelType), channelType) };

            var request = new RestRequest("channels", Method.POST);
            request.AddJsonBody(channel);

            return proxy.Execute<MMChannel>(request);
        }

        public void DeleteTeam(string teamname)
        {
            var team = GetTeamByTeamName(teamname);

            var request = new RestRequest("teams/{team_id}", Method.DELETE);
            request.AddParameter("team_id", team.id, ParameterType.UrlSegment);

            proxy.Execute<Object>(request);
        }

        public MMTeam RestoreTeam(string teamname)
        {
            var team = GetTeamByTeamName(teamname);
            var request = new RestRequest("teams/{team_id}/restore", Method.POST);
            request.AddParameter("team_id", team.id, ParameterType.UrlSegment);

            return proxy.Execute<MMTeam>(request);
        }

        public MMTeam CreateTeam(string teamname, string displayname, MMTeam.TeamType teamtype = MMTeam.TeamType.I)
        {
            Ensure.NotEmpty(teamname, "Teamname");
            Ensure.NotEmpty(displayname, "Display name");

            teamname = _sanitizeName(teamname); 

            if(TeamExists(teamname))
            {
                throw new Exception($"Team {teamname} already exists"); 
            }

            var request = new RestRequest("teams", Method.POST);
            MMTeam team = new MMTeam() { name = teamname, display_name = displayname, type = Enum.GetName(typeof(MMTeam.TeamType), teamtype) }; 

            request.AddJsonBody(team);

            return proxy.Execute<MMTeam>(request); 

        }

        public MMUser CreateEmailUser(string email, string username, string password, string firstname, string lastname)
        {
            Ensure.NotEmpty(email, "Email");
            Ensure.NotEmpty(username, "Username");
            Ensure.NotEmpty(password, "Password");

            MMUser user = new MMUser() { email = email, username = username, first_name = firstname, last_name = lastname, password = password };

            var request = new RestRequest("users", Method.POST);
            request.AddJsonBody(user); 
            return proxy.Execute<MMUser>(request);
        }

        public void EnableUser(string username)
        {
            var user = GetUserByUserName(username); 

            var request = new RestRequest("users/{user_id}/active", Method.PUT);
            request.AddParameter("user_id", user.id, ParameterType.UrlSegment);
            request.AddJsonBody(new { active = true });

            proxy.Execute<Object>(request); 

        }

        public void DeleteUser(string username)
        {
            Ensure.NotEmpty(username, "Username");

            var user = GetUserByUserName(username); 

            var request = new RestRequest("users/{user_id}", Method.DELETE);
            request.AddParameter("user_id", user.id, ParameterType.UrlSegment); 

            proxy.Execute<MMUser>(request);
        }
    }
}
