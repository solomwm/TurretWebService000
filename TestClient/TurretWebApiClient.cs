using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TurretWebService.Models;
using TurretWebService.Params;
using Newtonsoft.Json;

namespace TestClient
{
    public class TurretWebApiClient: HttpClient
    {
        private readonly string usersPath = "api/Users/";
        private readonly string path;

        public TurretWebApiClient(string baseAddress):base()
        {
            BaseAddress = new Uri(baseAddress);
            path = BaseAddress + usersPath;
            DefaultRequestHeaders.Accept.Clear();
            DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //this.DefaultRequestHeaders.UserAgent;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            IEnumerable<User> users = null;
            try
            {
                HttpResponseMessage response = await GetAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    users = await response.Content.ReadAsAsync<IEnumerable<User>>();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return users;
        }

        public async Task<IEnumerable<User>> GetUsersByNameOrContain(string nameOrContain, UsersSearchParam searchParam)
        {
            IEnumerable<User> users = null;
            string requestUri = String.Empty;

            switch (searchParam)
            {
                case UsersSearchParam.ByName: requestUri = path + "getbyname/" + nameOrContain;
                    break;
                case UsersSearchParam.IfContain: requestUri = path + "getifcontain/" + nameOrContain;
                    break;
                default: requestUri = path;
                    break;
            }

            try
            {
                HttpResponseMessage response = await GetAsync(requestUri);
                if (response.IsSuccessStatusCode)
                {
                    users = await response.Content.ReadAsAsync<IEnumerable<User>>();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return users;
        }

        public async Task<User> GetUserAsync(string id)
        {
            User user = null;
            try
            {
                HttpResponseMessage response = await GetAsync(path + id);
                if (response.IsSuccessStatusCode)
                {
                    user = await response.Content.ReadAsAsync<User>();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return user;
        }

        public async Task<User> GetUserAsync(int id)
        {
            string _id = id.ToString();
            return await GetUserAsync(_id);
        }

        public async Task DeleteUserAsync(string id)
        {
            try
            {
                HttpResponseMessage response = await DeleteAsync(path + id);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public async Task DeleteUserAsync(int id)
        {
            string _id = id.ToString();
            await DeleteUserAsync(_id);
        }

        public async Task<User> AddUserAsync(User user)
        {
            User result = null;
            HttpRequestMessage request = new HttpRequestMessage
            {
                Content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json")
            };
            try
            {
                HttpResponseMessage response = await PostAsync(path, request.Content);
                result = await response.Content.ReadAsAsync<User>();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return result;
        }

        public async Task EditUserAsync(string id, User user)
        {
            HttpRequestMessage request = new HttpRequestMessage
            {
                Content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json")
            };
            try
            {
                HttpResponseMessage responce = await PutAsync(path + id, request.Content);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public async Task EditUserAsync(int id, User user)
        {
            string _id = id.ToString();
            await EditUserAsync(_id, user);
        }
    }
}
