using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Web1.Models;
using System.Web;

namespace Web1.Controllers
{
    public class ModelsController : ApiController
    {
        

        public HttpResponseMessage Get()
        {
            string query = @"select ModelId,ModelName,Type_Chasi,convert(varchar(10),DateOfProduction,120) as DateOfProduction,PhotoFilename from dbo.Models";

            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["CarsDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);

            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        public string Post(Models_Cars mod)
        {
            try
            {
                string query = @"insert into dbo.Models values (

                    '" + mod.ModelName + @"'
                    ,'" + mod.Type_Chasi + @"'
                    ,'" + mod.DateOfProduction + @"'
                    ,'" + mod.PhotoFileName + @"'
                                        
                    )";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["CarsDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);

                }

                return "Added Successfully!";
            }
            catch (Exception)
            {
                return "Failed to Add!";
            }


        }


        public string Put(Models_Cars mod)
        {
            try
            {
                string query = @"update dbo.Models
                        
                set ModelName= '" + mod.ModelName + @"' 
               ,Type_Chasi= '" + mod.Type_Chasi + @"' 
               ,DateOfProduction= '" + mod.DateOfProduction + @"' 
               ,PhotoFileName= '" + mod.PhotoFileName + @"'  
                    where ModelId =" + mod.ModelId + @" ";


                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["CarsDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);

                }

                return "Updated Successfully!";
            }
            catch (Exception)
            {
                return "Failed to Update!";
            }
        }

        public string Delete(int id)
        {
            try
            {
                string query = @" delete from  dbo.Models 
                        where ModelId =" + id + @" ";


                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["CarsDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);

                }

                return "Deleted Successfully!";
            }
            catch (Exception)
            {
                return "Failed to Delete!";
            }
        }


        [Route("api/Models/GetAllManufacturerNames")]
        [HttpGet]

        public HttpResponseMessage GetAllManufacturerNames()
        {

            string query = @" 
                select ManufacturerName from dbo.Manufacturer";

            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["CarsDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);

            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }


        [Route("api/Models/SaveFile")]
       
        public string SaveFile()
        {
            try
            {
                var htttpRequest = HttpContext.Current.Request;
                var PostedFile = htttpRequest.Files[0];
                string filename = PostedFile.FileName;
                var physicalPath = HttpContext.Current.Server.MapPath("~/Photos/" + filename);

                PostedFile.SaveAs(physicalPath);

                return (filename + " Uploaded successfully");
            }
            catch (Exception)
            {

                return "Failed to upload / anonymous.png ?";
            }
        }

    }
}
