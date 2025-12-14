namespace CSEMockInterview.Tests;

public class UnitTest1
{
    [Theory]
    [InlineData(50, 50, 100)]
    [InlineData(23, 23, 46)]
    [InlineData(48, 23, 71)]
    public void Test1(int a, int b, int expected)
    {
        //arrange
        Operation service = new Operation();
        
        //act
        var result = service.math(a, b);
        
        //assert
        Assert.Equal(expected, result);
    }
}