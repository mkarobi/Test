For use test create class then add [Fact] on top of method
in method use three section
1- Arrange
2- act
3- assert
arrange => you choose your true result
act => use metod that test it and add parameter
assert => Assert.Equal(arrange,act);

For use multi test for method use below:

[Theory]
[InlineData(x1,y1,expected1)]
[InlineData(x2,y2,expected2)]
[InlineData(x3,y3,expected3)]
public void	add_shouldCalculate(double x,double y, double expected){

	double act = method(x,y);
	Assert.Equal(expected,act);
}


For Authorize we should add header to method:
ctor => {
_UnToken = "";
_AToken = "eyJhbGcJmLTRkYjb6T0SrCWk2X8uCSgtCHN7BdbtsPJjX8T2GtcxlQ3H8x-JCaCJ9tBaSV_VhA7M-Q";
}

in method =>{
_client.DefaultRequestHeaders.Authorization
           = new AuthenticationHeaderValue("Bearer", _UnToken);
}