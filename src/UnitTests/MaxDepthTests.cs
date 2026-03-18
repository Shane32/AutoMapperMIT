namespace AutoMapper.UnitTests;

public class MaxDepthTests
{
    public class Source
    {
        public int Level { get; set; }
        public IList<Source> Children { get; set; }
        public Source Parent { get; set; }

        public Source(int level)
        {
            Children = new List<Source>();
            Level = level;
        }

        public void AddChild(Source child)
        {
            Children.Add(child);
            child.Parent = this;
        }
    }

    public class Destination
    {
        public int Level { get; set; }
        public IList<Destination> Children { get; set; }
        public Destination Parent { get; set; }
    }

    private readonly Source _source;

    public MaxDepthTests()
    {
        var nest = new Source(1);

        nest.AddChild(new Source(2));
        nest.Children[0].AddChild(new Source(3));
        nest.Children[0].AddChild(new Source(3));
        nest.Children[0].Children[1].AddChild(new Source(4));
        nest.Children[0].Children[1].AddChild(new Source(4));
        nest.Children[0].Children[1].AddChild(new Source(4));

        nest.AddChild(new Source(2));
        nest.Children[1].AddChild(new Source(3));

        nest.AddChild(new Source(2));
        nest.Children[2].AddChild(new Source(3));

        _source = nest;
    }

    [Fact]
    public void Second_level_children_is_empty_with_max_depth_1()
    {
        var config = new MapperConfiguration(cfg => cfg.CreateMap<Source, Destination>().MaxDepth(1));
        var destination = config.CreateMapper().Map<Source, Destination>(_source);
        destination.Children.ShouldBeEmpty();
    }

    [Fact]
    public void Second_level_children_are_not_null_with_max_depth_2()
    {
        var config = new MapperConfiguration(cfg => cfg.CreateMap<Source, Destination>().MaxDepth(2));
        var destination = config.CreateMapper().Map<Source, Destination>(_source);
        foreach (var child in destination.Children)
        {
            2.ShouldBe(child.Level);
            child.ShouldNotBeNull();
            destination.ShouldBe(child.Parent);
        }
    }

    [Fact]
    public void Third_level_children_is_empty_with_max_depth_2()
    {
        var config = new MapperConfiguration(cfg => cfg.CreateMap<Source, Destination>().MaxDepth(2));
        var destination = config.CreateMapper().Map<Source, Destination>(_source);
        foreach (var child in destination.Children)
        {
            child.Children.ShouldBeEmpty();
        }
    }

    [Fact]
    public void Third_level_children_are_not_null_max_depth_3()
    {
        var config = new MapperConfiguration(cfg => cfg.CreateMap<Source, Destination>().MaxDepth(3));
        var destination = config.CreateMapper().Map<Source, Destination>(_source);
        foreach (var child in destination.Children)
        {
            child.Children.ShouldNotBeNull();
            foreach (var subChild in child.Children)
            {
                3.ShouldBe(subChild.Level);
                subChild.Children.ShouldNotBeNull();
                child.ShouldBe(subChild.Parent);
            }
        }
    }

    public class Circular
    {
        public Circular Self { get; set; }
    }

    [Fact]
    public void Deeply_nested_self_referential_should_not_stackoverflow()
    {
        var config = new MapperConfiguration(cfg => cfg.CreateMap<Circular, Circular>());
        var mapper = config.CreateMapper();

        var root = new Circular();
        var current = root;
        for (int i = 0; i < 100000; i++)
        {
            current.Self = new Circular();
            current = current.Self;
        }

        // Should not throw StackOverflowException
        var result = mapper.Map<Circular>(root);
        result.ShouldNotBeNull();
    }

    [Fact]
    public void Truly_circular_self_referential_should_not_stackoverflow()
    {
        var config = new MapperConfiguration(cfg => cfg.CreateMap<Circular, Circular>());
        var mapper = config.CreateMapper();

        // Create a truly circular reference: a -> b -> a
        var a = new Circular();
        var b = new Circular();
        a.Self = b;
        b.Self = a;

        // Should not throw StackOverflowException
        var result = mapper.Map<Circular>(a);
        result.ShouldNotBeNull();
        result.Self.ShouldNotBeNull();
    }
}
