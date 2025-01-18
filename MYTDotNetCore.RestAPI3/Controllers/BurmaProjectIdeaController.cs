﻿using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace MYTDotNetCore.RestAPI3.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BurmaProjectIdeaController : Controller
{
    private readonly HttpClient _httpClient;
    private readonly RestClient _restClient;
    private readonly ISnakeAPI _snakeApi;

    public BurmaProjectIdeaController(HttpClient httpClient, RestClient restClient, ISnakeAPI snakeApi)
    {
        _httpClient = httpClient;
        _restClient = restClient;
        _snakeApi = snakeApi;
    }

    [HttpGet("birds")]
    public async Task<IActionResult> BirdAsync()
    {
        var request = await _httpClient.GetAsync("bird");
        var response = await request.Content.ReadAsStringAsync();
        return Ok(response);
    }

    [HttpGet("pick-a-pile")]
    public async Task<IActionResult> PickPileAsync()
    {
        var request = new RestRequest("pick-a-pile", Method.Get);
        var response = await _restClient.GetAsync(request);
        return Ok(response.Content);
    }

    [HttpGet("snakes")]
    public async Task<IActionResult> SnakesAsync()
    {
        var response = await _snakeApi.GetSnakes();
        return Ok(response);
    }
}