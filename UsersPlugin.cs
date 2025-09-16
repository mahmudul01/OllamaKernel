using Microsoft.SemanticKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace OllamaKernel;

public class UsersPlugin
{
    private readonly List<UserModel> users;

    public UsersPlugin()
    {
        // Load users from JSON file
        var json = File.ReadAllText("users.json");
        users = JsonSerializer.Deserialize<List<UserModel>>(json) ?? new List<UserModel>();
    }

    [KernelFunction("get_users")]
    [Description("Gets the list of all users")]
    public async Task<List<UserModel>> GetUsersAsync()
    {
        return users;
    }

    [KernelFunction("get_user_by_id")]
    [Description("Gets a user by their ID")]
    public async Task<UserModel?> GetUserByIdAsync(int id)
    {
        return users.FirstOrDefault(u => u.Id == id);
    }

    [KernelFunction("get_user_by_email")]
    [Description("Gets a user by their email address")]
    public async Task<UserModel?> GetUserByEmailAsync(string email)
    {
        return users.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
    }

    [KernelFunction("get_user_by_phone")]
    [Description("Gets a user by their phone number")]
    public async Task<UserModel?> GetUserByPhoneAsync(string phone)
    {
        return users.FirstOrDefault(u => u.Phone.Equals(phone, StringComparison.OrdinalIgnoreCase));
    }
}

public class UserModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    [JsonPropertyName("phone")]
    public string Phone { get; set; } = string.Empty;
}