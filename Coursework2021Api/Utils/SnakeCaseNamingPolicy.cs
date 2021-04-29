using System.Linq;
using System.Text.Json;

namespace Coursework2021Api.Utils
{
    public class SnakeCasePropertyNamingPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name)
        {
            return string.Concat(name.Select((character, index) =>
                char.IsUpper(character) ?
                    (index == 0 ? "" : "_") + char.ToLower(character) :
                    character.ToString())
            );
        }
    }
}