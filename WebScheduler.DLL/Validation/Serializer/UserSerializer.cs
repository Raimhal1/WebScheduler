using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebScheduler.BLL.DtoModels;

namespace WebScheduler.BLL.Validation.Serializer
{
    public static class UserSerializer
    {
        public static string Serialize(UserDto user)
        {

            return JsonSerializer.Serialize<UserDto>(user, GetOptions());
        }

        private static JsonSerializerOptions GetOptions()
        {
            return new JsonSerializerOptions
            {
                WriteIndented = true
            };
        }
            
    }
}
