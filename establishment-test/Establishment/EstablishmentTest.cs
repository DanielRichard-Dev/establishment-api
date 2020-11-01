using establishment_models.Establishment;
using establishment_repository.Interface;
using establishment_service.Establishment;
using establishment_service.Interface;
using establishment_service.Validate;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace establishment_test.Establishment
{
    public class EstablishmentTest
    {
        private Mock<IEstablishmentRepository> _establishmnetRepository{ get; set; }
        private Mock<IEstablishmentAddressRepository> _establishmentAddressRepository { get; set; }
        private Mock<IEstablishmentAccountRepository> _establishmentAccountRepository { get; set; }
        private EstablishmentService _establishmentService { get; set; }
        public ValidateService _validateService { get; set; }

        public EstablishmentTest()
        {
            _establishmnetRepository = new Mock<IEstablishmentRepository>();
            _establishmentAddressRepository = new Mock<IEstablishmentAddressRepository>();
            _establishmentAccountRepository = new Mock<IEstablishmentAccountRepository>();
            _validateService = new ValidateService();
            _establishmentService = new EstablishmentService(_establishmnetRepository.Object, _establishmentAddressRepository.Object,
                _establishmentAccountRepository.Object, _validateService);
            SetupMock();
        }

        public void SetupMock()
        {
            _establishmnetRepository.Setup(x => x.InsertEstablishment(It.IsAny<EstablishmentModel>())).Returns(() => {
                return 1;
            });

            _establishmnetRepository.Setup(x => x.EstablishmentExist(It.IsIn<string>("37349399000120"))).Returns(() => {
                return 1;
            });
        }

        [Fact]
        public void Test_CreateEstablishment_Sucess()
        {
            var establishment = new EstablishmentModel
            {
                CompanyName = "TestComapany",
                FantasyName = "TestFantasy",
                CNPJ = "64766215000109",
                Email = "teste@teste.com",
                Telephone = "9874563213",
                Status = true,
                Address = new EstablishmentAddressModel
                { 
                    Address = "TestAddres",
                    City = "TestCity",
                    State = "TestState"
                },
                CategoryId = 3,
                Account = new EstablishmentAccountModel
                { 
                    Account = "123456",
                    Agency = "321564"
                }
            };

            var establishmentId = _establishmentService.CreateEstablishment(establishment);

            Assert.True(establishmentId != 0);
        }

        [Fact]
        public void Test_CreateEstablishment_Error()
        {
            var establishment = new EstablishmentModel
            {
                EstablishmentId = 1,
                CompanyName = "TestComapany",
                FantasyName = "TestFantasy",
                CNPJ = "37349399000120",
                Email = "teste@teste.com",
                Telephone = "9874563213",
                Status = true,
                Address = new EstablishmentAddressModel
                {
                    Address = "TestAddres",
                    City = "TestCity",
                    State = "TestState"
                },
                CategoryId = 3,
                Account = new EstablishmentAccountModel
                {
                    Account = "123456",
                    Agency = "321564"
                }
            };

            Assert.Throws<ArgumentException>(() => _establishmentService.CreateEstablishment(establishment));
        }
    }
}
