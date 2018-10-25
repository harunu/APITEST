using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Net;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace APITestProject
{
    [TestClass]
    public class APITest
    {

        /*  Ensure the API returns 401 when a client makes a request without auth. */
        [TestMethod]
        public void AuthTestMethod()
        {
            //Arrange
            var client = new Client();
            client.EndPoint = @"https://api.trivago.com/webservice/hotelmanager";
            client.Method = Verb.GET;
            //Act
            string response = client.Request("/pcs/123");
            var result = JsonConvert.DeserializeObject<ResponseJson>(response);
            //Assert
            Assert.AreEqual(result.title, "An application token is required.");
        }
        /* Ensure the API returns 401 when a client makes a request with the wrong auth. */
        [TestMethod]
        public void AuthTestMethodWrongAuth()
        {
            //Arrange
            var client = new Client();
            client.EndPoint = @"https://api.trivago.com/webservice/hotelmanager";
            client.Method = Verb.GET;
            client.Token = "Basic 8bad5c5480594635b00380dceb6d08b9";
            //Act
            string response = client.Request("/pcs/123");
            var result = JsonConvert.DeserializeObject<ResponseJson>(response);
            //Assert
            Assert.AreEqual(result.title, "The application token is invalid.");
        }
        /*Ensure the API returns 404 when a client makes a request an endpoint which does not exist.*/
        [TestMethod]
        public void AuthTestMethod404()
        {
            //Arrange
            var client = new Client();
            client.EndPoint = @"https://api.trivago.com/webservice/hotelmanager";
            client.Method = Verb.GET;
            client.Token = "Basic 57f666f533588cd988ad82c693fc328c";
            //Act
            string response = client.Request("/pcs/123/5");
            var result = JsonConvert.DeserializeObject<ResponseJson>(response);
            //Assert
            Assert.AreEqual(result.title, "The requested resource does not exist.");
        }
    }
}
    

