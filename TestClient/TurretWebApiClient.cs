using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TurretWebService.Models;

namespace TestClient
{
    public class TurretWebApiClient: HttpClient
    {
        public TurretWebApiClient(string baseAddress):base()
        {
            this.BaseAddress = new Uri(baseAddress);
            this.DefaultRequestHeaders.Accept.Clear();
            this.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IEnumerable<User>> GetUsersAsync(string path)
        {
            IEnumerable<User> users = null;
            try
            {
                HttpResponseMessage response = await base.GetAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    users = await response.Content.ReadAsAsync<IEnumerable<User>>();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, this.ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return users;
        }

        public async Task<User> GetUserAsync(string path)
        {
            User user = null;
            try
            {
                HttpResponseMessage response = await base.GetAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    user = await response.Content.ReadAsAsync<User>();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, this.ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return user;
        }
    }
}
