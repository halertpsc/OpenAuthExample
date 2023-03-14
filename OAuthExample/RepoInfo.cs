using Newtonsoft.Json;

namespace OAuthExample
{
    public record RepoInfo ([property: JsonProperty("id")]string id, [property: JsonProperty("name")]string name, [property: JsonProperty("full_name")]string fullName);
}
