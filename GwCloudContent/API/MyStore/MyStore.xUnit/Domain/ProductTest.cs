using Xunit;
using FluentAssertions;
using MyStore.xUnit.Builders;

namespace MyStore.xUnit.Domain;

public class ProductTest
{
    [Fact]
    public void Product_Should_BeValid()
    {
        var product = ProductFluent.New()
            .Build();

        product.IsValid.Should().BeTrue();
    }

    [Fact]
    public void ProductName_ShouldNot_BeEmpty()
    {
        var product = ProductFluent.New()
            .WithName("")
            .Build();

        product.IsValid.Should().BeFalse();
    }

    [Fact]
    public void ProductName_ShouldNot_BeNull()
    {
        var product = ProductFluent.New()
            .WithName(null)
            .Build();

        product.IsValid.Should().BeFalse();
    }

    [Fact]
    public void ProductNameLength_hould_BeGreaterOrEqualThan10()
    {
        var product = ProductFluent.New()
            .WithName("less than")
            .Build();

        product.IsValid.Should().BeFalse();
    }

    [Fact]
    public void ProductNameLength_Should_BeLessrOrEqualThan40()
    {
        var product = ProductFluent.New()
            .WithName("ABCDEFGHIJKLMNOPQRSTUVXYWZABCDEFGHIJKLMNOPQRSTUVXYWZ")
            .Build();

        product.IsValid.Should().BeFalse();
    }

    [Fact]
    public void ProductPrice_Should_BeGreaterThan0()
    {
        var product = ProductFluent.New()
            .WithPrice(0.00m)
            .Build();

        product.IsValid.Should().BeFalse();
    }
}