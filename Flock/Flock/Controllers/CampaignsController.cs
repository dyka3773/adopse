﻿using Flock.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flock.Models;
using MySql.Data.MySqlClient;
using SendEmail;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Flock.Controllers
{
    [Route("apis/[controller]")]
    [ApiController]
    public class CampaignsController : ControllerBase
    {


        public CampaignsController()
        {
            


        }

        // GET: api/<CampaignsController>
        [HttpPost("sendCampaign/{caid}")]
        public void sendCampaign(int caid)
        {
            EmailService email = new EmailService(caid);

            string fr = email.getFrequency();
            

            email.mailSender();
        }

        // GET api/<CampaignsController>/5
        [HttpGet("GetNumOfPages/{aid}")]
        public decimal GetNumOfPages(int aid)
        {
            int numOfRows = 1;
            

            List<Campaign> campaigns = new List<Campaign>();
            using var cmd = new MySqlCommand();
            cmd.Connection = new DBConnection().connect();
            cmd.Connection.Open();

            cmd.CommandText = String.Format("call numOfPagesInCamps({0},null,{1})", aid,numOfRows);

            MySqlDataReader reader = cmd.ExecuteReader();
            reader.Read();

            return (decimal)reader.GetValue(0);
        }

        // GET api/<CampaignsController>/5
        [HttpGet("{aid}/{pageNum}/{query?}")]
        public List<Campaign> Get(int aid, int pageNum,string query)
        {
            int numOfRows = 5;
            int offset = numOfRows * pageNum;

            List<Campaign> campaigns = new List<Campaign>();
            using var cmd = new MySqlCommand();
            cmd.Connection = new DBConnection().connect();
            cmd.Connection.Open();

            if (query == null)
            {
                cmd.CommandText = String.Format("call getCamps({0}, null, {1}, {2})", aid, offset, numOfRows);
            }
            else {
                cmd.CommandText = String.Format("call getCamps({0}, '{1}', {2}, {3})", aid, query, offset, numOfRows);
            }
            
           
            
           

            MySqlDataReader reader = cmd.ExecuteReader();


            while (reader.Read())
            {

                campaigns.Add(new Campaign
                {
                    id = (int)reader.GetValue(0),
                    subject = reader.GetValue(1).ToString(),
                    text = reader.GetValue(2).ToString(),
                    startDate = reader.GetValue(3).ToString(),
                    endDate = reader.GetValue(4).ToString(),
                    creationDate = reader.GetValue(5).ToString(),
                    name = reader.GetValue(6).ToString(),
                    frequency = reader.GetValue(7).ToString(),
                    numOfContacts = (uint)reader.GetValue(8),
                    AID = (int)reader.GetValue(10),
                    GID = (int)reader.GetValue(11)
                });
            }


            cmd.Connection.Close();
            return campaigns;
        }


        public Campaign getCampFromCaid(int caid)
        {
            Campaign campaign;
            using var cmd = new MySqlCommand();
            cmd.Connection = new DBConnection().connect();
            cmd.Connection.Open();

            cmd.CommandText = "getCampFromCAID(" + caid + ")";

            MySqlDataReader reader = cmd.ExecuteReader();


            reader.Read();



            campaign = new Campaign
            {
                id = (int)reader.GetValue(0),
                subject = reader.GetValue(1).ToString(),
                text = reader.GetValue(2).ToString(),
                startDate = reader.GetValue(3).ToString(),
                endDate = reader.GetValue(4).ToString(),
                creationDate = reader.GetValue(5).ToString(),
                name = reader.GetValue(6).ToString(),
                frequency = reader.GetValue(7).ToString(),
                numOfContacts = (uint)reader.GetValue(8),
                AID = (int)reader.GetValue(10),
                GID = (int)reader.GetValue(11)
            };



            cmd.Connection.Close();
            return campaign;
        }




        // POST api/<CampaignsController>
        [HttpPost("{aid}/{gid}")]
        public void Post(Campaign camp, int aid, int gid)
        {
            using var cmd = new MySqlCommand();
            cmd.Connection = new DBConnection().connect();
            cmd.Connection.Open();

            cmd.CommandText = String.Format("call addCampaign('{0}', '{1}', '{2}', '{3}','{4}', '{5}','{6}', {7} , {8} )",
                camp.subject, camp.text, camp.startDate, camp.endDate, camp.name, camp.frequency, null, aid, gid);
            MySqlDataReader reader = cmd.ExecuteReader();


            cmd.Connection.Close();
        }

        // PUT api/<CampaignsController>/5
        [HttpPut("{id}")]
        public void Put(int id, Campaign camp)
        {
            using var cmd = new MySqlCommand();
            cmd.Connection = new DBConnection().connect();
            cmd.Connection.Open();

            cmd.CommandText = String.Format("call editCampaign({0}, '{1}', {2}, '{3}', '{4}', '{5}', '{6}')", camp.id, camp.name, id, camp.subject, camp.text, camp.endDate, camp.frequency);
            MySqlDataReader reader = cmd.ExecuteReader();


            cmd.Connection.Close();


        }

        // DELETE api/<CampaignsController>/5
        [HttpDelete("{id}/{cid}")]
        public void Delete(int id, int cid)
        {
            using var cmd = new MySqlCommand();
            cmd.Connection = new DBConnection().connect();
            cmd.Connection.Open();

            cmd.CommandText = String.Format("call deleteCamp({0}, {1})", cid, id);

            MySqlDataReader reader = cmd.ExecuteReader();

        }
    }
}
