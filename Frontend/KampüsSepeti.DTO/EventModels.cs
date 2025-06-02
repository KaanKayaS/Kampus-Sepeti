using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace KampüsSepeti.DTO
{
    public class EventApiResponse
    {
        public Meta meta { get; set; }
        public List<Event> items { get; set; }
    }

    public class Meta
    {
        public int total_count { get; set; }
    }

    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Url { get; set; }
        public string Content { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool IsFree { get; set; }
        [JsonPropertyName("poster_url")]
        public string PosterUrl { get; set; }
        [JsonPropertyName("ticket_url")]
        public string TicketUrl { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        [JsonPropertyName("facebook_url")]
        public string FacebookUrl { get; set; }
        [JsonPropertyName("twitter_url")]
        public string TwitterUrl { get; set; }
        public string Hashtag { get; set; }
        [JsonPropertyName("web_url")]
        public string WebUrl { get; set; }
        [JsonPropertyName("live_url")]
        public string LiveUrl { get; set; }
        [JsonPropertyName("android_url")]
        public string AndroidUrl { get; set; }
        [JsonPropertyName("ios_url")]
        public string IosUrl { get; set; }
        public Format Format { get; set; }
        public Category Category { get; set; }
        public Venue Venue { get; set; }
        public List<Tag> Tags { get; set; }
    }

    public class Format
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
    }

    public class Category
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("slug")]
        public string Slug { get; set; }
    }

    public class Venue
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string About { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
        public int Status { get; set; }
        public string Phone { get; set; }
        [JsonPropertyName("web_url")]
        public string WebUrl { get; set; }
        [JsonPropertyName("facebook_url")]
        public string FacebookUrl { get; set; }
        [JsonPropertyName("twitter_url")]
        public string TwitterUrl { get; set; }
        public City City { get; set; }
        public District District { get; set; }
        public Neighborhood Neighborhood { get; set; }
        public string Address { get; set; }
    }

    public class City
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("slug")]
        public string Slug { get; set; }
    }

    public class District
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
    }

    public class Neighborhood
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
    }

    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
    }

    // Filtreleme için ViewModel
    public class EventFilterViewModel
    {
        public List<City> Cities { get; set; }
        public List<Category> Categories { get; set; }
        public List<Event> Events { get; set; }
        public int? SelectedCityId { get; set; }
        public int? SelectedCategoryId { get; set; }
        public string SearchTerm { get; set; }
    }
} 