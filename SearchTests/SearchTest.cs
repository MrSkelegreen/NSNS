using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NewNsns.ViewModels;
using Newtonsoft.Json;

namespace SearchTests;
using NewNsns;

[TestClass]
public class SearchTest : MainViewModel
{

    [TestMethod]
    public void EmptyField_Test()
    {
        //arrange

        string testUserInput = null;
        string expectedMessage = "Field is empty.\n" +
                                 "Type something like: 200 g chicken";

        //act

        MainViewModel m = new MainViewModel();
        m.UserInput = testUserInput;
        m.ChangeMessage();

        //assert

        Assert.AreEqual(expectedMessage, m.Message);

    }

}