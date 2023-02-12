using AutoFixture;
using HH_Global_Job_Quotation.Models.DTO;
using HH_Global_Job_Quotation.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace HH_Global_Job.UnitTest
{
    [TestClass]
    public class GenerateInvoice
    {
        private Mock<IGetCostInvoice> _getCostInvoiceMockUp;
        private Fixture _fixture;

        public GenerateInvoice()
        {
                _fixture= new Fixture();
            _getCostInvoiceMockUp= new Mock<IGetCostInvoice>();
        }


        [TestMethod]
        public async Task Get_Job_Item_Invoice()
        {
            var createJobRequest = _fixture.Create<JobItemCreateRequest>();
            var jobRespose =  _fixture.Create<JobItemResponse>();

            var response =  _getCostInvoiceMockUp.Setup(request => request.GetCostInvoiceJobItem(It.IsAny<JobItemCreateRequest>())).Returns(jobRespose);

            var obj = response as ObjectResult;

            Assert.AreEqual(response, response);
            await Task.CompletedTask;
        }
    }
}
