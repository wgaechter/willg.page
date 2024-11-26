using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using Octokit;
using PortfolioWebsite.Models;
using System.Collections.Generic;
using System.Dynamic;
using System.Net.WebSockets;
using System.Reflection;
using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using Google.Apis.Gmail.v1.Data;

namespace PortfolioWebsite.Pages
{
    public class ProjectsModel : PageModel
    {
        public List<RepoModel> _projects;
        private readonly SQLiteContext _context;

        public ProjectsModel(SQLiteContext context)
        {
            _context = context;
            _projects = GetProjects();
        }

        //Local pull used to source _projects list
        public List<RepoModel> GetProjects()
        {
            List<RepoModel> repositories = new List<RepoModel>();
            repositories =  _context.Projects.ToList();
            return repositories;
        }

        public void OnGet()
        {

        }

        public Dictionary<string, string> _language_icons = new Dictionary<string, string>
        {
                    { "Git", "<i class='devicon-git-plain' style='color: #555'></i>" },
                    { "Github", "<i class='devicon-github-plain' style='color: #1688f0'></i>" },
                    { "Chrome", "<i class='devicon-chrome-plain' style='color: #1688f0'></i>" },
                    { "Assembly", "<i class='devicon-labview-plain colored'></i> Assembly" },
                    { "C#", "<i class='devicon-csharp-plain colored'></i> C#" },
                    { "C++", "<i class='devicon-cplusplus-plain colored'></i> C++" },
                    { "C", "<i class='devicon-c-plain colored'></i> C" },
                    { "Clojure", "<i class='devicon-clojure-plain colored'></i> Clojure" },
                    { "CoffeeScript", "<i class='devicon-coffeescript-plain colored'></i> CoffeeScript" },
                    { "Crystal", "<i class='devicon-crystal-plain colored'></i> Crystal" },
                    { "CSS", "<i class='devicon-css3-plain colored'></i> CSS" },
                    { "Dart", "<i class='devicon-dart-plain colored'></i> Dart" },
                    { "Dockerfile", "<i class='devicon-docker-plain colored'></i> Dockerfile" },
                    { "Elixir", "<i class='devicon-elixir-plain colored'></i> Elixir" },
                    { "Elm", "<i class='devicon-elm-plain colored'></i> Elm" },
                    { "Erlang", "<i class='devicon-erlang-plain colored'></i> Erlang" },
                    { "F#", "<i class='devicon-fsharp-plain colored'></i> F#" },
                    { "Go", "<i class='devicon-go-plain colored'></i> Go" },
                    { "Groovy", "<i class='devicon-groovy-plain colored'></i> Groovy" },
                    { "HTML", "<i class='devicon-html5-plain colored'></i> HTML" },
                    { "Haskell", "<i class='devicon-haskell-plain colored'></i> Haskell" },
                    { "Java", "<i class='devicon-java-plain colored' style='color: #ffca2c'></i> Java" },
                    { "JavaScript", "<i class='devicon-javascript-plain colored'></i> JavaScript" },
                    { "Julia", "<i class='devicon-julia-plain colored'></i> Julia" },
                    { "Jupyter Notebook", "<i class='devicon-jupyter-plain colored'></i> Jupyter Notebook" },
                    { "Kotlin", "<i class='devicon-kotlin-plain colored' style='color: #796bdc'></i> Kotlin" },
                    { "Latex", "<i class='devicon-latex-plain colored'></i> Latex" },
                    { "Lua", "<i class='devicon-lua-plain-wordmark colored' style='color: #0000d0'></i> Lua" },
                    { "Matlab", "<i class='devicon-matlab-plain colored'></i> Matlab" },
                    { "Nim", "<i class='devicon-nixos-plain colored' style='color: #FFC200'></i> Nim" },
                    { "Nix", "<i class='devicon-nixos-plain colored'></i> Nix" },
                    { "ObjectiveC", "<i class='devicon-objectivec-plain colored'></i> Objective-C" },
                    { "OCaml", "<i class='devicon-ocaml-plain colored'></i> OCaml" },
                    { "Perl", "<i class='devicon-perl-plain colored'></i> Perl" },
                    { "PHP", "<i class='devicon-php-plain colored'></i> PHP" },
                    { "PLSQL", "<i class='devicon-sqlite-plain colored'></i> PLSQL" },
                    { "Processing", "<i class='devicon-processing-plain colored' style='color: #0096D8'></i> Processing" },
                    { "Python", "<i class='devicon-python-plain colored' style='color: #3472a6'></i> Python" },
                    { "R", "<i class='devicon-r-plain colored'></i> R" },
                    { "Ruby", "<i class='devicon-ruby-plain colored'></i> Ruby" },
                    { "Rust", "<i class='devicon-rust-plain colored' style='color: #DEA584'></i> Rust" },
                    { "Sass", "<i class='devicon-sass-original colored'></i> Sass" },
                    { "Scala", "<i class='devicon-scala-plain colored'></i> Scala" },
                    { "Shell", "<i class='devicon-bash-plain colored' style='color: #89E051'></i> Shell" },
                    { "Solidity", "<i class='devicon-solidity-plain colored'></i> Solidity" },
                    { "Stylus", "<i class='devicon-stylus-plain colored'></i> Stylus" },
                    { "Svelte", "<i class='devicon-svelte-plain colored'></i> Svelte" },
                    { "Swift", "<i class='devicon-swift-plain colored'></i> Swift" },
                    { "Terraform", "<i class='devicon-terraform-plain colored'></i> Terraform" },
                    { "TypeScript", "<i class='devicon-typescript-plain colored'></i> TypeScript" },
                    { "Vim Script", "<i class='devicon-vim-plain colored'></i> Vim Script" },
                    { "Vue", "<i class='devicon-vuejs-plain colored'></i> Vue" }
        };
    }
}
