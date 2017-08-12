using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace GParser
{
    /**
     * The A-Parser API Client
     **/
    public class Aparser
    {
        //The password for the A-Parser API
        private string _password;

        //The base URL to use for call to the API
        private string _host;

        protected Dictionary<string, string> _options = new Dictionary<string, string>();

        public Aparser(string host, string password, Dictionary<string,string> options)
        {
            _password = password;
            _host = host;
        }

        //Adds a new option value with a default value
        public void AddOption(string name, string value = null)
        {
            _options[name] = value; 
        }

        //Changes an option value
        public void SetOption(string name, string value)
        {
            if (!_options.ContainsKey(name))
            {
                throw new ArgumentException( String.Format( "{0} does not support the following option: {1}", GetType().Name, name) );
            }
            _options[name] = value;
        }

        //Gets an option value
        protected string GetOption(string name)
        {
            return _options.ContainsKey(name) ? _options[name] : null;
        }

        //Check if the option exist
        protected bool HasOption(string name)
        {
            return _options.ContainsKey(name);
        }

        //Configures the current object 
        //This method allows set options during object creation
        protected void Configure(Dictionary<string, string> options)
        {
            foreach (KeyValuePair<string, string> keyValue in options)
            {
                AddOption(keyValue.Key, keyValue.Value);
            }

        }

        public void SetPassword(string password)
        {
            _password = password;
        }
        
        public void SetHost(string host)
        {
            _host = host;
        }

        protected string GetPassword()
        {
            if (_password.Length == 0)
            {
                throw new ArgumentException("Current Password is incorrect!");
            }
            return _password;
        }

        protected string GetHost()
        {
            if (_host.Length == 0)
            {
                throw new ArgumentException("Current URL is incorrect!");
            }
            return _host;
        }

        //The ping method, the server should respond by invoking "pong" on
        //the callback data
        public async Task<dynamic> Ping()
        {
            RequestHelper request = new RequestHelper(_password, _host);
            return await request.MakeRequestAsync("ping");
        }
        //Return total information(pid, version, tasks in queue);
        public async Task<dynamic> Info()
        {
            RequestHelper request = new RequestHelper(_password, _host);
            return  await request.MakeRequestAsync("info");
        }
        //Getting a list of live proxies
        public async Task<dynamic> GetProxies()
        {
            RequestHelper request = new RequestHelper(_password, _host);
            return await request.MakeRequestAsync("getProxies");
        }
        //Single request parsing, you can use any parser and preset.This
        //will generate the strings in accordance with the format of the
        //result set in the preset, as well as the full log parser.
        public async Task<dynamic> OneRequest(string query, string parser, string preset = "default",  int rawResults = 0)
        {
            RequestHelper request = new RequestHelper(_password, _host);
            dynamic data = new {
                query = query,
                parser = parser,
                preset = preset,
                rawResults = rawResults
            };
            return await request.MakeRequestAsync("oneRequest", data );
        }

        //Bulk request parsing, you can use any parser and preset, as well
        //as the quantity indicated in the threads to produce parsing.
        //This will generate the strings in accordance with the format of
        //the result set in the preset, as well as the full log parser for
        //each thread.
        public async Task<dynamic> BulkRequest(string[] queries, string parser, string preset = "default", int rawResults = 0, int threads = 5)
        {
            RequestHelper request = new RequestHelper(_password, _host);
            dynamic data = new
            {
                queries = queries,
                parser = parser,
                preset = preset,
                rawResults = rawResults,
                threads = threads
            };
            return await request.MakeRequestAsync("bulkRequest", data);
        }

        //Getting of the parser settings and presets
        public async Task<dynamic> GetParserPreset(string parser, string preset = "default")
        {
            RequestHelper request = new RequestHelper(_password, _host);
            dynamic data = new
            {
                parser = parser,
                preset = preset
            };
            return await request.MakeRequestAsync("getParserPreset", data);
        }
        //Add a task to turn all options are similar to those that are
        //specified in the interface Add Task
        public async Task<dynamic> AddTask( string taskPreset, string queriesFrom, Dictionary<string, string> queries, string configPreset = "default")
        {
            Dictionary<string, string> options = new Dictionary<string, string>() { };
            return await AddTask(taskPreset, queriesFrom, queries, options, configPreset);
        }

        public async Task<dynamic> AddTask( string taskPreset,string queriesFrom, Dictionary<string,string> queries, Dictionary<string, string> options, string configPreset = "default")
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            if (taskPreset.Length != 0)
            {
                data.Add("preset", taskPreset);
            }
            else
            {
                data.Add("resultsFileName",  options.ContainsKey("resultsFileName") ? options["resultsFileName"] : "$datefile.format().txt");
                data.Add("parsers", options.ContainsKey("parsers") ? options["parsers"] : (object)new Dictionary<string, string>());
                data.Add("uniqueQueries", options.ContainsKey("uniqueQueries") ? options["uniqueQueries"] : (object)0);
                data.Add("keepUnique", options.ContainsKey("keepUnique") ? options["keepUnique"] : (object)0);
                data.Add("resultsPrepend", options.ContainsKey("resultsPrepend") ? options["resultsPrepend"]  : "");
                data.Add("moreOptions", options.ContainsKey("moreOptions") ? options["moreOptions"] : "");
                data.Add("resultsUnique", options.ContainsKey("resultsUnique") ? options["resultsUnique"] : "no");
                data.Add("doLog", options.ContainsKey("doLog") ? options["doLog"] : "no");
                data.Add("queryFormat", options.ContainsKey("queryFormat") ? options["queryFormat"] : "query");
                data.Add("resultsSaveTo", options.ContainsKey("resultsSaveTo") ? options["resultsSaveTo"] : "file");
                data.Add("configOverrides", options.ContainsKey("configOverrides") ? options["configOverrides"] : (object)new Dictionary<string, string>());
                data.Add("resultsFormat", options.ContainsKey("resultsFormat") ? options["resultsFormat"]   : "");
                data.Add("resultsAppend", options.ContainsKey("resultsAppend") ? options["resultsAppend"]   : "");
                data.Add("queryBuilders", options.ContainsKey("queryBuilders") ? options["queryBuilders"]   : (object)new Dictionary<string, string>());
                data.Add("resultsBuilders", options.ContainsKey("resultsBuilders") ? options["resultsBuilders"] : (object)new Dictionary<string, string>());
            }

            switch (queriesFrom)
            {
                case "file":
                    data.Add("queriesFrom", "file");
                    data.Add("resultsFileName", options.ContainsKey("queriesFile") ? options["queriesFile"] : (object)false);
                    break;
                case "text":
                    data.Add("queriesFrom", "text");
                    data.Add("resultsFileName", queries.Count != 0 ? queries  : (object)new Dictionary<string, string>());
                    break;
                default:
                    throw new ArgumentException("Argument queriesForm is incorrect!");
            }
            RequestHelper request = new RequestHelper(_password, _host);
            return await request.MakeRequestAsync("addTask", data);
        }

        //Getting the status of task by uid
        public async Task<dynamic> GetTaskState(int taskUid)
        {
            RequestHelper request = new RequestHelper(_password, _host);
            dynamic data = new
            {
                taskUid = taskUid
            };
            return await request.MakeRequestAsync("getTaskState", data);
        }

        //Getting configuration task by uid
        public async Task<dynamic> GetTaskConf(int taskUid)
        {
            RequestHelper request = new RequestHelper(_password, _host);
            dynamic data = new
            {
                taskUid = taskUid
            };
            return await request.MakeRequestAsync("getTaskConf", data);
        }

        //Change status of a task by id
        //toStatus starting|pausing|stopping|deleting
        public async Task<dynamic> ChangeTaskStatus(int taskUid, string toStatus)
        {
            RequestHelper request = new RequestHelper(_password, _host);
            dynamic data = new
            {
                taskUid = taskUid,
                toStatus = toStatus
            };
            return await request.MakeRequestAsync("changeTaskStatus", data);
        }

        //direction start|end|up|down
        public async Task<dynamic> MoveTask(int taskUid, string direction)
        {
            RequestHelper request = new RequestHelper(_password, _host);
            dynamic data = new
            {
                taskUid = taskUid,
                direction = direction
            };
            return await request.MakeRequestAsync("moveTask", data);
        }

        //Getting the link to Task results file by Task Uid
        public async Task<dynamic> GetTaskResultsFile(int taskUid)
        {
            RequestHelper request = new RequestHelper(_password, _host);
            dynamic data = new
            {
                taskUid = taskUid
            };
            return await request.MakeRequestAsync("getTaskResultsFile", data);
        }

        //Removing results file by Task Uid
        public async Task<dynamic> DeleteTaskResultsFile(int taskUid)
        {
            RequestHelper request = new RequestHelper(_password, _host);
            dynamic data = new
            {
                taskUid = taskUid
            };
            return await request.MakeRequestAsync("deleteTaskResultsFile", data);
        }

        // Getting the list of tasks
        public async Task<dynamic> GetTasksList(int completed = 0)
        {
            RequestHelper request = new RequestHelper(_password, _host);
            dynamic data = new
            {
                completed = completed
            };
            return await request.MakeRequestAsync("getTasksList", data);
        }
        //Displays a list of all available results that can return the specified parser.
        public async Task<dynamic> GetParserInfo(string parser)
        {
            RequestHelper request = new RequestHelper(_password, _host);
            dynamic data = new
            {
                parser = parser
            };
            return await request.MakeRequestAsync("getParserInfo", data);
        }

        // Update executable file of the parser to the latest version, after sending the command.
        public async Task<dynamic> Update(string parser)
        {
            RequestHelper request = new RequestHelper(_password, _host);
            return await request.MakeRequestAsync("update");
        }

        //Getting the number of active accounts (for Yandex).
        public async Task<dynamic> GetAccountsCount(string parser)
        {
            RequestHelper request = new RequestHelper(_password, _host);
            return await request.MakeRequestAsync("getAccountsCount");
        }

    }
}
