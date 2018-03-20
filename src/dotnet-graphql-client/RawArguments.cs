namespace GraphQL.DotNet.GraphQL.Client {

    class RawArguments{

        public string EndPoint = string.Empty;

        public string Query = string.Empty;

        public Modee Mode= Modee.POST;

        public bool Pretty = false;

        public enum Modee {
            GET,
            POST
        }

    }

}
