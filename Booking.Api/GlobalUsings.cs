global using System;
global using System.Threading.Tasks;
global using System.Text.RegularExpressions;
global using System.Net;

global using Microsoft.AspNetCore.Hosting;
global using Microsoft.Extensions.Hosting;
global using Microsoft.Extensions.Options;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.OpenApi.Models;
global using Microsoft.AspNetCore.Authorization;

global using Booking.Api.Middleware;
global using Booking.Shared.Services.Interfaces;
global using Booking.Shared.Entities;
global using Booking.Shared.Repositories.Interfaces;
global using Booking.Shared.Exceptions;
global using Booking.Shared.Models.Config;
global using Booking.Shared.Mapper;
global using Booking.Shared.Repositories;
global using Booking.Shared.Models.DataTransferObjects;
global using Booking.Shared.Models;
global using Booking.Shared.Extensions;

global using Google.Cloud.Firestore;

global using Newtonsoft.Json;
global using Newtonsoft.Json.Linq;

