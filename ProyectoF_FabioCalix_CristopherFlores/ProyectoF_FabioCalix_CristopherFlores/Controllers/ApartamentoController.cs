﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web.Http;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using ProyectoF_FabioCalix_CristopherFlores.Models;

namespace ProyectoF_FabioCalix_CristopherFlores.Controllers
{
    public class ApartamentoController : ApiController
    {
        [HttpGet]
        public async Task<HttpResponseMessage> Get(DataSourceLoadOptions loadOptions)
        {
            var apiUrl = "https://localhost:44331/api/Apartamento";

            var respuestaJson = await GetAsync(apiUrl);
            List<Apartamento> listApartamento = JsonConvert.DeserializeObject<List<Apartamento>>(respuestaJson);
            return Request.CreateResponse(DataSourceLoader.Load(listApartamento, loadOptions));
        }

        public static async Task<string> GetAsync(string uri)
        {
            try
            {
                var handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
                using (var client = new HttpClient(handler))
                {
                    var response = await client.GetAsync(uri);
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsStringAsync();
                }

            }
            catch (Exception e)
            {
                var m = e.Message;
                return null;
            }

        }

        [HttpPost]
        public async Task<HttpResponseMessage> Post(FormDataCollection form)
        {

            var values = form.Get("values");

            var httpContent = new StringContent(values, System.Text.Encoding.UTF8, "application/json");

            var url = "https://localhost:44331/api/Apartamento";
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
            using (var client = new HttpClient(handler))
            {
                var response = await client.PostAsync(url, httpContent);

                var result = response.Content.ReadAsStringAsync().Result;
            }

            return Request.CreateResponse(HttpStatusCode.Created);
        }


        [HttpDelete]
        public async Task<HttpResponseMessage> Delete(FormDataCollection form)
        {
            var key = Convert.ToInt32(form.Get("key"));

            var apiUrlDelApto = "https://localhost:44331/api/Apartamento/" + key;
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
            using (var client = new HttpClient(handler))
            {
                var respuestaApto = await client.DeleteAsync(apiUrlDelApto);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }


        [HttpPut]
        public async Task<HttpResponseMessage> Put(FormDataCollection form)
        {
            var key = Convert.ToInt32(form.Get("key"));
            var values = form.Get("values");

            var apiUrlGetApto = $"https://localhost:44331/api/Apartamento/{key}";
            var respuestaApartamento = await GetAsync(apiUrlGetApto);
            Apartamento apartamento = JsonConvert.DeserializeObject<Apartamento>(respuestaApartamento);

            JsonConvert.PopulateObject(values, apartamento);

            string jsonString = JsonConvert.SerializeObject(apartamento);
            var httpContent = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");

            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
            using (var client = new HttpClient(handler))
            {
                var url = $"https://localhost:44331/api/Apartamento/{key}";
                var response = await client.PutAsync(url, httpContent);

                var result = response.Content.ReadAsStringAsync().Result;
            }


            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}