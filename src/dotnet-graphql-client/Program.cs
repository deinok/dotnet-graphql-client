using System;
using System.CommandLine;
using Newtonsoft.Json;

namespace GraphQL.DotNet.GraphQL.Client {

    class Program {

        public static void Main(string[] args) {
            var endpoint = string.Empty;
            var query = string.Empty;
            var mode = Mode.POST;
            var pretty = false;
            var argumentSyntax=ArgumentSyntax.Parse(args, syntax =>{
                {
                    syntax.DefineOption("e|endpoint", ref endpoint, true, help: "The endpoint to target");
                    if (!Uri.IsWellFormedUriString(endpoint, UriKind.RelativeOrAbsolute)) {
                        syntax.ReportError("Not a valid endpoint");
                    }
                }
                {
                    syntax.DefineOption("q|query", ref query, true, help: "The GraphQL Query to send");
                }
                {
                    syntax.DefineOption("m|mode", ref mode, (str) => { return Mode.POST; }, help: "The Mode to send the GraphQL");
                }
                {
                    syntax.DefineOption("p|pretty", ref pretty, help: "JSON pretty");
                }
            });

            using (var graphqlClient=new global::GraphQL.Client.GraphQLClient(endpoint)) {
                var graphqlResponse= graphqlClient.PostQueryAsync(query).Result;
                var jsonString=JsonConvert.SerializeObject(graphqlResponse, pretty ? Formatting.Indented : Formatting.None);
                Console.Out.Write(jsonString);
            }

        }

        enum Mode {
            GET,
            POST
        }


    }

}
