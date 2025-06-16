using Microsoft.EntityFrameworkCore;
using test2apbd.Models;

namespace test2apbd.Data;

public class DatabaseContext :DbContext
{
    protected DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<Character> Characters { get; set; } = null!;
    public DbSet<Item> Items { get; set; } = null!;
    public DbSet<Title> Titles { get; set; } = null!;
    public DbSet<Backpack> Backpacks { get; set; } = null!;
    public DbSet<CharacterTitle> CharacterTitles { get; set; } = null!;
     protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Backpack>()
                .HasKey(b => new { b.CharacterId, b.ItemId });

            modelBuilder.Entity<CharacterTitle>()
                .HasKey(ct => new { ct.CharacterId, ct.TitleId });

            modelBuilder.Entity<Character>().HasData(
                new Character {
                    CharacterId   = 1,
                    FirstName     = "Danylo",
                    LastName      = "Antypenko",
                    CurrentWeight = 3,
                    MaxWeight     = 20
                },
                new Character {
                    CharacterId   = 2,
                    FirstName     = "Hello",
                    LastName      = "World",
                    CurrentWeight = 5,
                    MaxWeight     = 25
                }
            );

            modelBuilder.Entity<Item>().HasData(
                new Item {
                    ItemId = 1,
                    Name   = "Mystic Sword",
                    Weight = 5
                },
                new Item {
                    ItemId = 2,
                    Name   = "Potion",
                    Weight = 1
                },
                new Item {
                    ItemId = 3,
                    Name   = "Armor",
                    Weight = 8
                }
            );

            modelBuilder.Entity<Title>().HasData(
                new Title {
                    TitleId = 1,
                    Name    = "Enemy1"
                },
                new Title {
                    TitleId = 2,
                    Name    = "Enemy2"
                },
                new Title {
                    TitleId = 3,
                    Name    = "Enemy3"
                }
            );

            modelBuilder.Entity<Backpack>().HasData(
                new Backpack {
                    CharacterId = 1,
                    ItemId      = 1,
                    Amount      = 1
                },
                new Backpack {
                    CharacterId = 1,
                    ItemId      = 2,
                    Amount      = 2
                },
                new Backpack {
                    CharacterId = 2,
                    ItemId      = 3,
                    Amount      = 1
                },
                new Backpack {
                    CharacterId = 2,
                    ItemId      = 2,
                    Amount      = 1
                }
            );

            modelBuilder.Entity<CharacterTitle>().HasData(
                new CharacterTitle {
                    CharacterId = 1,
                    TitleId     = 1,
                    AcquiredAt  = new DateTime(2025, 1,  1, 9, 30, 0)
                },
                new CharacterTitle {
                    CharacterId = 1,
                    TitleId     = 2,
                    AcquiredAt  = new DateTime(2025, 2, 15, 14, 0, 0)
                },
                new CharacterTitle {
                    CharacterId = 2,
                    TitleId     = 1,
                    AcquiredAt  = new DateTime(2025, 1,  5, 11, 0, 0)
                },
                new CharacterTitle {
                    CharacterId = 2,
                    TitleId     = 3,
                    AcquiredAt  = new DateTime(2025, 3, 20, 16, 45, 0)
                }
            );
        }
    }