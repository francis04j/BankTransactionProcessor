using Shouldly;
using TransactionProcesor.Utils;

namespace TransactionProcessor.UnitTests.Utils;

public class ItemsReverserTests
{
    private readonly ItemsReverser sut;

    public ItemsReverserTests()
    {
        sut = new ItemsReverser();
    }

    [Fact]
    public void ShouldReverseItemsCorrectly()
    {
        //given
        StringInput input = "Hello World";
        string expected = "dlroW olleH";
        
        //when
       var result = sut.Reverse(input);
       //Action action = () => sut.Reverse(input);
        
        //then
        result.ShouldBeEquivalentTo(expected);
        //Should.Throw<NotImplementedException>(action);
    }
    
    [Fact]
    public void ShouldIgnoreEmptyInput()
    {
        //given
        string input = "";
        string expected = "";
        
        //when
        var result = sut.Reverse(input);
       
        
        //then
        result.ShouldBeEquivalentTo(expected);
    }
    
    [Fact]
    public void ShouldReverseSingleWordsCorrectly()
    {
        //given
        string input = "hello";
        string expected = "olleh";
        
        //when
        var result = sut.Reverse(input);
       
        
        //then
        result.ShouldBeEquivalentTo(expected);
    }
    
    [Fact]
    public void ShouldRespectCaseWhenReversing()
    {
        //given
        string input = "heLLo";
        string expected = "oLLeh";
        
        //when
        var result = sut.Reverse(input);
       
        //then
        result.ShouldBeEquivalentTo(expected);
    }
    
    [Fact]
    public void ShouldReverseAListOfWordsCorrectly()
    {
        //given
        List<string> input = new List<string>() { "heLLo", "hi", "my", "name", "is" };
        var expected = new List<string>() { "is", "name", "my", "hi", "heLLo"  };
        
        //when
        var result = sut.Reverse(input);
       
        //then
        result.ShouldBeEquivalentTo(expected);
    }
    
    [Fact]
    public void ShouldReverseAListOfLettersCorrectly()
    {
        //given
        int [] input =  { 1,2,3,4,5 };
        int [] expected =  { 5,4,3,2,1 };
        
        //when
        var result = sut.Reverse(input);
       
        //then
        result.ShouldBeEquivalentTo(expected);
    }
}