﻿using Flock.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flock.Models;
using MySql.Data.MySqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Flock.Controllers
{

    [Route("apis/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        public CompaniesController() {
            /*companies.Add(new Company { id = 1, name="pp", phone=(long)306969696969, country="ppstan", zip="56625", phyAddress="yeeeNo" });
            companies.Add(new Company { id = 2, name = "pp2", phone = (long)306969696969, country = "ppst22an", zip = "5662225", phyAddress = "yeeeNo" });
            companies.Add(new Company { id = 3, name = "pp3", phone = (long)306969696969, country = "pps2313tan", zip = "56625", phyAddress = "yee123eNo" });
            companies.Add(new Company { id = 4, name = "pp4", phone = (long)306969696969, country = "ppst123an", zip = "56625", phyAddress = "yeeeNo" });*/
        }

        // GET: apis/<CompanyController>
        [HttpGet]
        public List<Company> Get()
        {
            return null;
        }

        // GET apis/<CompanyController>/5
        [HttpGet("{id}")]
        public List<Company> Get(int id)
        {
            List<Company> companies = new List<Company>();
            using var cmd = new MySqlCommand();
            cmd.Connection = new DBConnection().connect();
            cmd.Connection.Open();

            cmd.CommandText = "getAccDetails(" + id + ")";

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {

                companies.Add(new Company { email = reader.GetValue(0).ToString(),
                                            password = reader.GetValue(1).ToString(),
                                            type = (int)reader.GetValue(2),
                                            numOfCamps = (int)reader.GetValue(3),
                                            numOfConts = (int)reader.GetValue(4),
                                            numOfSent = (int)reader.GetValue(5),
                                            id = (int)reader.GetValue(6),
                                            name = reader.GetValue(7).ToString(),
                                            phone = (long)reader.GetValue(8),
                                            country = reader.GetValue(9).ToString(),
                                            zip = reader.GetValue(10).ToString(),
                                            phyAddress = reader.GetValue(11).ToString()
                });

            }

            cmd.Connection.Close();
            return companies;
        }

        // POST api/<CompanyController>
        [HttpPost]
        public void Post([FromBody] Company comp)
        {
        }

        // PUT api/<CompanyController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CompanyController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
