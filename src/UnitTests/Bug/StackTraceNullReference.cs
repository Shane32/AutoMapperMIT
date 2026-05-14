namespace AutoMapper.UnitTests.Bug;

public class StackTraceNullReference : NonValidatingSpecBase
{
    public class Source
    {
        public decimal Foo { get; set; }
    }

    public class Destination
    {
        public Type Foo { get; set; }   // type mismatch triggers DryRunTypeMap failure
        public string Bar { get; set; } // unmapped member triggers badTypeMaps path
    }

    protected override MapperConfiguration CreateConfiguration() => new(cfg =>
        cfg.CreateMap<Source, Destination>());

    [Fact]
    public void StackTrace_throws_NullReferenceException_when_exception_was_never_thrown()
    {
        // AssertConfigurationIsValid produces an AggregateException with two inner exceptions:
        // [0] AutoMapperConfigurationException created via new(badTypeMaps) — never directly thrown
        // [1] AutoMapperConfigurationException for the type mismatch on Foo
        var aex = new Action(AssertConfigurationIsValid).ShouldThrow<AggregateException>();
        var inner = aex.InnerExceptions[0].ShouldBeOfType<AutoMapperConfigurationException>();
        inner.Errors.ShouldNotBeNull();

        // base.StackTrace is null because the exception was added to a list but never thrown.
        // The StackTrace override calls base.StackTrace.Split(...) without a null check,
        // which throws NullReferenceException.
        new Action(() => _ = inner.StackTrace).ShouldThrow<NullReferenceException>();
    }
}
