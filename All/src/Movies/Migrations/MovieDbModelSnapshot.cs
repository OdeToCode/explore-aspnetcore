using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Relational.Migrations.Infrastructure;
using Movies.Services;

namespace Movies.Migrations
{
    [ContextType(typeof(MovieDb))]
    partial class MovieDbModelSnapshot : ModelSnapshot
    {
        public override void BuildModel(ModelBuilder builder)
        {
            builder
                .Annotation("SqlServer:DefaultSequenceName", "DefaultSequence")
                .Annotation("SqlServer:Sequence:.DefaultSequence", "'DefaultSequence', '', '1', '10', '', '', 'Int64', 'False'")
                .Annotation("SqlServer:ValueGeneration", "Sequence");
            
            builder.Entity("Movies.Entities.Movie", b =>
                {
                    b.Property<int>("Id")
                        .GenerateValueOnAdd()
                        .StoreGeneratedPattern(StoreGeneratedPattern.Identity);
                    
                    b.Property<int>("Length");
                    
                    b.Property<DateTime>("Release");
                    
                    b.Property<string>("Title");
                    
                    b.Key("Id");
                });
            
            builder.Entity("Movies.Entities.Review", b =>
                {
                    b.Property<int>("Id")
                        .GenerateValueOnAdd()
                        .StoreGeneratedPattern(StoreGeneratedPattern.Identity);
                    
                    b.Property<string>("Body");
                    
                    b.Property<int?>("MovieId");
                    
                    b.Property<string>("User");
                    
                    b.Key("Id");
                });
            
            builder.Entity("Movies.Entities.Review", b =>
                {
                    b.Reference("Movies.Entities.Movie")
                        .InverseCollection()
                        .ForeignKey("MovieId");
                });
        }
    }
}
