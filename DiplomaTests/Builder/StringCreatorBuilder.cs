using System;
using System.Collections.Generic;
using System.Text;
using Diploma;
using Diploma.Utils;
using Diploma.Wrappers;
using Moq;

namespace DiplomaTests.Builder
{
    public class StringCreatorBuilder
    {
        private Mock<IConstants> constants;
        private Mock<IDomainWrapper> domainWrapper;

        public StringCreatorBuilder()
        {
            this.constants = new Mock<IConstants>();
            this.domainWrapper = new Mock<IDomainWrapper>();
        }

        public StringCreatorBuilder WithConstants(Mock<IConstants> constants)
        {
            this.constants = constants;
            return this;
        }

        public StringCreatorBuilder WithDomain(Mock<IDomainWrapper> wrapper)
        {
            this.domainWrapper = wrapper;
            return this;
        }

        public StringCreator Build()
        {
            return new StringCreator(constants.Object, domainWrapper.Object);
        }
    }
}
