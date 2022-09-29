
using System.Reflection;
using NUnit.Framework;
using WaveFunction.Modding;

namespace WaveFunctionTest.Modding
{
    public class LibDiscoveryTest
    {
        [Test]
        public void Library_Discovery_Will_Attempt_To_Retrieve_Instance_Of_IPlugin_From_The_Library()
        {
            //Arrange
            //Act
            var plugins = LibDiscoverer.GetPluginsForFolder("/Mods/");
            //Assert
            Assert.That(plugins.Length, Is.EqualTo(1));
            Assert.That(plugins[0].Name(), Is.EqualTo("Hello, World!"));
            Assert.That(plugins[0].GetType().Name, Is.EqualTo("Class1"));
        }
    }
}
