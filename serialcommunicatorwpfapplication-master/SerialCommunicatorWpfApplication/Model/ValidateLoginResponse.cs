using System;
using System.Collections.Generic;

namespace SerialCommunicatorWpfApplication.Model
{
    public class ValidateLoginResponse
    {
        public int? error { get; set; }
        public Data? data { get; set; }
    }
    public class City
    {
        public int? id { get; set; }
        public string? title { get; set; }
    }

    public class Data
    {
        public User? user { get; set; }
        public string? access_token { get; set; }
        public string? api_key { get; set; }
    }

    public class Permission
    {
        public int? id { get; set; }
        public string? title { get; set; }
        public string? system { get; set; }
        public string? group { get; set; }
        public string? created_at { get; set; }
        public string? updated_at { get; set; }
        public string? description { get; set; }
        public string? persian_title { get; set; }
        public Pivot? pivot { get; set; }
    }

    public class Pivot
    {
        public int? user_id { get; set; }
        public int? role_id { get; set; }
        public int? permission_id { get; set; }
    }

    public class Role
    {
        public int? id { get; set; }
        public string? parent_id { get; set; }
        public string? title { get; set; }
        public string? description { get; set; }
        public string? created_at { get; set; }
        public string? updated_at { get; set; }
        public string? persian_title { get; set; }
        public Pivot? pivot { get; set; }
        public List<Permission>? permissions { get; set; }
    }

    public class State
    {
        public int? id { get; set; }
        public string? title { get; set; }
    }

    public class User
    {
        public int? id { get; set; }
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public string? father_name { get; set; }
        public string? national_code { get; set; }
        public string? mobile { get; set; }
        public string? gender { get; set; }
        public string? email { get; set; }
        public string? sheba { get; set; }
        public int? married { get; set; }
        public string? education { get; set; }
        public string? job { get; set; }
        public int? state_id { get; set; }
        public int? city_id { get; set; }
        public string? address { get; set; }
        public string? birthday { get; set; }
        public int? status { get; set; }
        public string? created_at { get; set; }
        public string? contact_info { get; set; }
        public int? parent_id { get; set; }
        public bool? has_water_meter { get; set; }
        public List<Role>? roles { get; set; }
        public State? state { get; set; }
        public City? city { get; set; }
    }
}
