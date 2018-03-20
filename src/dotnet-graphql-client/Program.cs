using System;
using System.CommandLine;
using Newtonsoft.Json;

namespace GraphQL.DotNet.GraphQL.Client {

    class Program {

        public static void Main(string[] args) {
            var rawArguments = new RawArguments();
            var argumentSyntax = ArgumentSyntax.Parse(args, syntax => {
                {
                    syntax.DefineOption("e|endpoint", ref rawArguments.EndPoint, true, help: "The endpoint to target");
                    if (!Uri.IsWellFormedUriString(rawArguments.EndPoint, UriKind.RelativeOrAbsolute)) {
                        syntax.ReportError("Not a valid endpoint");
                    }
                }
                {
                    syntax.DefineOption("q|query", ref rawArguments.Query, true, help: "The GraphQL Query to send");
                }
                {
                    syntax.DefineOption("m|mode", ref rawArguments.Mode, (str) => { return RawArguments.Modee.POST; }, help: "The Mode to send the GraphQL");
                }
                {
                    syntax.DefineOption("p|pretty", ref rawArguments.Pretty, help: "JSON pretty");
                }
            });

            using (var graphqlClient = new global::GraphQL.Client.GraphQLClient(rawArguments.EndPoint)) {
                var graphqlResponse = graphqlClient.PostQueryAsync(rawArguments.Query).Result;
                var jsonString = JsonConvert.SerializeObject(graphqlResponse, rawArguments.Pretty ? Formatting.Indented : Formatting.None);
                Console.Out.Write(jsonString);
            }

        }

    }

}
