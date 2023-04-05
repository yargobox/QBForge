using System.Collections.Generic;

namespace QBForge.Providers
{
	internal partial class PostgreSqlProvider
	{
		/// <summary>
		/// Whether the specified value must be quoted as a keyword (reserved, non-reserved but requires As, etc.)
		/// </summary>
		/// <param name="value">Lower case word</param>
		private static bool IsQuotedKeyword(string value)
		{
			return Cache.QuotedKeywords.Contains(value);
		}

		private static class Cache
		{
			static Cache() { }

			// 9th February 2023: PostgreSQL 15.2
			// https://www.postgresql.org/docs/current/sql-keywords-appendix.html
			// except blanks, non-reserved, and non-reserved (cannot be function or type)
			//
			public static readonly HashSet<string> QuotedKeywords = new()
			{
				// "a",
				// "abort", // non-reserved
				// "abs",
				// "absent",
				// "absolute", // non-reserved
				// "access", // non-reserved
				// "according",
				// "acos",
				// "action", // non-reserved
				// "ada",
				// "add", // non-reserved
				// "admin", // non-reserved
				// "after", // non-reserved
				// "aggregate", // non-reserved
				"all", // reserved
				// "allocate",
				// "also", // non-reserved
				// "alter", // non-reserved
				// "always", // non-reserved
				"analyse", // reserved
				"analyze", // reserved
				"and", // reserved
				"any", // reserved
				// "are",
				"array", // reserved, requires as
				// "array_agg",
				// "array_​max_​cardinality",
				"as", // reserved, requires as
				"asc", // reserved
				// "asensitive", // non-reserved
				// "asin",
				// "assertion", // non-reserved
				// "assignment", // non-reserved
				"asymmetric", // reserved
				// "at", // non-reserved
				// "atan",
				// "atomic", // non-reserved
				// "attach", // non-reserved
				// "attribute", // non-reserved
				// "attributes",
				"authorization", // reserved (can be function or type)
				// "avg",
				// "backward", // non-reserved
				// "base64",
				// "before", // non-reserved
				// "begin", // non-reserved
				// "begin_frame",
				// "begin_partition",
				// "bernoulli",
				// "between", // non-reserved (cannot be function or type)
				// "bigint", // non-reserved (cannot be function or type)
				"binary", // reserved (can be function or type)
				// "bit", // non-reserved (cannot be function or type)
				// "bit_length",
				// "blob",
				// "blocked",
				// "bom",
				// "boolean", // non-reserved (cannot be function or type)
				"both", // reserved
				// "breadth", // non-reserved
				// "by", // non-reserved
				// "c",
				// "cache", // non-reserved
				// "call", // non-reserved
				// "called", // non-reserved
				// "cardinality",
				// "cascade", // non-reserved
				// "cascaded", // non-reserved
				"case", // reserved
				"cast", // reserved
				// "catalog", // non-reserved
				// "catalog_name",
				// "ceil",
				// "ceiling",
				// "chain", // non-reserved
				// "chaining",
				"char", // non-reserved (cannot be function or type), requires as
				"character", // non-reserved (cannot be function or type), requires as
				// "characteristics", // non-reserved
				// "characters",
				// "character_length",
				// "character_​set_​catalog",
				// "character_set_name",
				// "character_set_schema",
				// "char_length",
				"check", // reserved
				// "checkpoint", // non-reserved
				// "class", // non-reserved
				// "classifier",
				// "class_origin",
				// "clob",
				// "close", // non-reserved
				// "cluster", // non-reserved
				// "coalesce", // non-reserved (cannot be function or type)
				// "cobol",
				"collate", // reserved
				"collation", // reserved (can be function or type)
				// "collation_catalog",
				// "collation_name",
				// "collation_schema",
				// "collect",
				"column", // reserved
				// "columns", // non-reserved
				// "column_name",
				// "command_function",
				// "command_​function_​code",
				// "comment", // non-reserved
				// "comments", // non-reserved
				// "commit", // non-reserved
				// "committed", // non-reserved
				// "compression", // non-reserved
				"concurrently", // reserved (can be function or type)
				// "condition",
				// "conditional",
				// "condition_number",
				// "configuration", // non-reserved
				// "conflict", // non-reserved
				// "connect",
				// "connection", // non-reserved
				// "connection_name",
				"constraint", // reserved
				// "constraints", // non-reserved
				// "constraint_catalog",
				// "constraint_name",
				// "constraint_schema",
				// "constructor",
				// "contains",
				// "content", // non-reserved
				// "continue", // non-reserved
				// "control",
				// "conversion", // non-reserved
				// "convert",
				// "copy", // non-reserved
				// "corr",
				// "corresponding",
				// "cos",
				// "cosh",
				// "cost", // non-reserved
				// "count",
				// "covar_pop",
				// "covar_samp",
				"create", // reserved, requires as
				"cross", // reserved (can be function or type)
				// "csv", // non-reserved
				// "cube", // non-reserved
				// "cume_dist",
				// "current", // non-reserved
				"current_catalog", // reserved
				"current_date", // reserved
				// "current_​default_​transform_​group",
				// "current_path",
				"current_role", // reserved
				// "current_row",
				"current_schema", // reserved (can be function or type)
				"current_time", // reserved
				"current_timestamp", // reserved
				// "current_​transform_​group_​for_​type",
				"current_user", // reserved
				// "cursor", // non-reserved
				// "cursor_name",
				// "cycle", // non-reserved
				// "data", // non-reserved
				// "database", // non-reserved
				// "datalink",
				// "date",
				// "datetime_​interval_​code",
				// "datetime_​interval_​precision",
				"day", // non-reserved, requires as
				// "db",
				// "deallocate", // non-reserved
				// "dec", // non-reserved (cannot be function or type)
				// "decfloat",
				// "decimal", // non-reserved (cannot be function or type)
				// "declare", // non-reserved
				"default", // reserved
				// "defaults", // non-reserved
				"deferrable", // reserved
				// "deferred", // non-reserved
				// "define",
				// "defined",
				// "definer", // non-reserved
				// "degree",
				// "delete", // non-reserved
				// "delimiter", // non-reserved
				// "delimiters", // non-reserved
				// "dense_rank",
				// "depends", // non-reserved
				// "depth", // non-reserved
				// "deref",
				// "derived",
				"desc", // reserved
				// "describe",
				// "descriptor",
				// "detach", // non-reserved
				// "deterministic",
				// "diagnostics",
				// "dictionary", // non-reserved
				// "disable", // non-reserved
				// "discard", // non-reserved
				// "disconnect",
				// "dispatch",
				"distinct", // reserved
				// "dlnewcopy",
				// "dlpreviouscopy",
				// "dlurlcomplete",
				// "dlurlcompleteonly",
				// "dlurlcompletewrite",
				// "dlurlpath",
				// "dlurlpathonly",
				// "dlurlpathwrite",
				// "dlurlscheme",
				// "dlurlserver",
				// "dlvalue",
				"do", // reserved
				// "document", // non-reserved
				// "domain", // non-reserved
				// "double", // non-reserved
				// "drop", // non-reserved
				// "dynamic",
				// "dynamic_function",
				// "dynamic_​function_​code",
				// "each", // non-reserved
				// "element",
				"else", // reserved
				// "empty",
				// "enable", // non-reserved
				// "encoding", // non-reserved
				// "encrypted", // non-reserved
				"end", // reserved
				"end-exec", // *
				// "end_frame",
				// "end_partition",
				// "enforced",
				// "enum", // non-reserved
				// "equals",
				// "error",
				// "escape", // non-reserved
				// "event", // non-reserved
				// "every",
				"except", // reserved, requires as
				// "exception",
				// "exclude", // non-reserved
				// "excluding", // non-reserved
				// "exclusive", // non-reserved
				// "exec",
				// "execute", // non-reserved
				// "exists", // non-reserved (cannot be function or type)
				// "exp",
				// "explain", // non-reserved
				// "expression", // non-reserved
				// "extension", // non-reserved
				// "external", // non-reserved
				// "extract", // non-reserved (cannot be function or type)
				"false", // reserved
				// "family", // non-reserved
				"fetch", // reserved, requires as
				// "file",
				"filter", // non-reserved, requires as
				// "final",
				// "finalize", // non-reserved
				// "finish",
				// "first", // non-reserved
				// "first_value",
				// "flag",
				// "float", // non-reserved (cannot be function or type)
				// "floor",
				// "following", // non-reserved
				"for", // reserved, requires as
				// "force", // non-reserved
				"foreign", // reserved
				// "format",
				// "fortran",
				// "forward", // non-reserved
				// "found",
				// "frame_row",
				// "free",
				"freeze", // reserved (can be function or type)
				"from", // reserved, requires as
				// "fs",
				// "fulfill",
				"full", // reserved (can be function or type)
				// "function", // non-reserved
				// "functions", // non-reserved
				// "fusion",
				// "g",
				// "general",
				// "generated", // non-reserved
				// "get",
				// "global", // non-reserved
				// "go",
				// "goto",
				"grant", // reserved, requires as
				// "granted", // non-reserved
				// "greatest", // non-reserved (cannot be function or type)
				"group", // reserved, requires as
				// "grouping", // non-reserved (cannot be function or type)
				// "groups", // non-reserved
				// "handler", // non-reserved
				"having", // reserved, requires as
				// "header", // non-reserved
				// "hex",
				// "hierarchy",
				// "hold", // non-reserved
				"hour", // non-reserved, requires as
				// "id",
				// "identity", // non-reserved
				// "if", // non-reserved
				// "ignore",
				"ilike", // reserved (can be function or type)
				// "immediate", // non-reserved
				// "immediately",
				// "immutable", // non-reserved
				// "implementation",
				// "implicit", // non-reserved
				// "import", // non-reserved
				"in", // reserved
				// "include", // non-reserved
				// "including", // non-reserved
				// "increment", // non-reserved
				// "indent",
				// "index", // non-reserved
				// "indexes", // non-reserved
				// "indicator",
				// "inherit", // non-reserved
				// "inherits", // non-reserved
				// "initial",
				"initially", // reserved
				// "inline", // non-reserved
				"inner", // reserved (can be function or type)
				// "inout", // non-reserved (cannot be function or type)
				// "input", // non-reserved
				// "insensitive", // non-reserved
				// "insert", // non-reserved
				// "instance",
				// "instantiable",
				// "instead", // non-reserved
				// "int", // non-reserved (cannot be function or type)
				// "integer", // non-reserved (cannot be function or type)
				// "integrity",
				"intersect", // reserved, requires as
				// "intersection",
				// "interval", // non-reserved (cannot be function or type)
				"into", // reserved, requires as
				// "invoker", // non-reserved
				"is", // reserved (can be function or type)
				"isnull", // reserved (can be function or type), requires as
				// "isolation", // non-reserved
				"join", // reserved (can be function or type)
				// "json_array",
				// "json_arrayagg",
				// "json_exists",
				// "json_object",
				// "json_objectagg",
				// "json_query",
				// "json_table",
				// "json_table_primitive",
				// "json_value",
				// "k",
				// "keep",
				// "key", // non-reserved
				// "keys",
				// "key_member",
				// "key_type",
				// "label", // non-reserved
				// "lag",
				// "language", // non-reserved
				// "large", // non-reserved
				// "last", // non-reserved
				// "last_value",
				"lateral", // reserved
				// "lead",
				"leading", // reserved
				// "leakproof", // non-reserved
				// "least", // non-reserved (cannot be function or type)
				"left", // reserved (can be function or type)
				// "length",
				// "level", // non-reserved
				// "library",
				"like", // reserved (can be function or type)
				// "like_regex",
				"limit", // reserved, requires as
				// "link",
				// "listagg",
				// "listen", // non-reserved
				// "ln",
				// "load", // non-reserved
				// "local", // non-reserved
				"localtime", // reserved
				"localtimestamp", // reserved
				// "location", // non-reserved
				// "locator",
				// "lock", // non-reserved
				// "locked", // non-reserved
				// "log",
				// "log10",
				// "logged", // non-reserved
				// "lower",
				// "m",
				// "map",
				// "mapping", // non-reserved
				// "match", // non-reserved
				// "matched", // non-reserved
				// "matches",
				// "match_number",
				// "match_recognize",
				// "materialized", // non-reserved
				// "max",
				// "maxvalue", // non-reserved
				// "measures",
				// "member",
				// "merge", // non-reserved
				// "message_length",
				// "message_octet_length",
				// "message_text",
				// "method", // non-reserved
				// "min",
				"minute", // non-reserved, requires as
				// "minvalue", // non-reserved
				// "mod",
				// "mode", // non-reserved
				// "modifies",
				// "module",
				"month", // non-reserved, requires as
				// "more",
				// "move", // non-reserved
				// "multiset",
				// "mumps",
				// "name", // non-reserved
				// "names", // non-reserved
				// "namespace",
				// "national", // non-reserved (cannot be function or type)
				"natural", // reserved (can be function or type)
				// "nchar", // non-reserved (cannot be function or type)
				// "nclob",
				// "nested",
				// "nesting",
				// "new", // non-reserved
				// "next", // non-reserved
				// "nfc", // non-reserved
				// "nfd", // non-reserved
				// "nfkc", // non-reserved
				// "nfkd", // non-reserved
				// "nil",
				// "no", // non-reserved
				// "none", // non-reserved (cannot be function or type)
				// "normalize", // non-reserved (cannot be function or type)
				// "normalized", // non-reserved
				"not", // reserved
				// "nothing", // non-reserved
				// "notify", // non-reserved
				"notnull", // reserved (can be function or type), requires as
				// "nowait", // non-reserved
				// "nth_value",
				// "ntile",
				"null", // reserved
				// "nullable",
				// "nullif", // non-reserved (cannot be function or type)
				// "nulls", // non-reserved
				// "null_ordering",
				// "number",
				// "numeric", // non-reserved (cannot be function or type)
				// "object", // non-reserved
				// "occurrence",
				// "occurrences_regex",
				// "octets",
				// "octet_length",
				// "of", // non-reserved
				// "off", // non-reserved
				"offset", // reserved, requires as
				// "oids", // non-reserved
				// "old", // non-reserved
				// "omit",
				"on", // reserved, requires as
				// "one",
				"only", // reserved
				// "open",
				// "operator", // non-reserved
				// "option", // non-reserved
				// "options", // non-reserved
				"or", // reserved
				"order", // reserved, requires as
				// "ordering",
				// "ordinality", // non-reserved
				// "others", // non-reserved
				// "out", // non-reserved (cannot be function or type)
				"outer", // reserved (can be function or type)
				// "output",
				"over", // non-reserved, requires as
				// "overflow",
				"overlaps", // reserved (can be function or type), requires as
				// "overlay", // non-reserved (cannot be function or type)
				// "overriding", // non-reserved
				// "owned", // non-reserved
				// "owner", // non-reserved
				// "p",
				// "pad",
				// "parallel", // non-reserved
				// "parameter", // non-reserved
				// "parameter_mode",
				// "parameter_name",
				// "parameter_​ordinal_​position",
				// "parameter_​specific_​catalog",
				// "parameter_​specific_​name",
				// "parameter_​specific_​schema",
				// "parser", // non-reserved
				// "partial", // non-reserved
				// "partition", // non-reserved
				// "pascal",
				// "pass",
				// "passing", // non-reserved
				// "passthrough",
				// "password", // non-reserved
				// "past",
				// "path",
				// "pattern",
				// "per",
				// "percent",
				// "percentile_cont",
				// "percentile_disc",
				// "percent_rank",
				// "period",
				// "permission",
				// "permute",
				// "pipe",
				"placing", // reserved
				// "plan",
				// "plans", // non-reserved
				// "pli",
				// "policy", // non-reserved
				// "portion",
				// "position", // non-reserved (cannot be function or type)
				// "position_regex",
				// "power",
				// "precedes",
				// "preceding", // non-reserved
				"precision", // non-reserved (cannot be function or type), requires as
				// "prepare", // non-reserved
				// "prepared", // non-reserved
				// "preserve", // non-reserved
				// "prev",
				"primary", // reserved
				// "prior", // non-reserved
				// "private",
				// "privileges", // non-reserved
				// "procedural", // non-reserved
				// "procedure", // non-reserved
				// "procedures", // non-reserved
				// "program", // non-reserved
				// "prune",
				// "ptf",
				// "public",
				// "publication", // non-reserved
				// "quote", // non-reserved
				// "quotes",
				// "range", // non-reserved
				// "rank",
				// "read", // non-reserved
				// "reads",
				// "real", // non-reserved (cannot be function or type)
				// "reassign", // non-reserved
				// "recheck", // non-reserved
				// "recovery",
				// "recursive", // non-reserved
				// "ref", // non-reserved
				"references", // reserved
				// "referencing", // non-reserved
				// "refresh", // non-reserved
				// "regr_avgx",
				// "regr_avgy",
				// "regr_count",
				// "regr_intercept",
				// "regr_r2",
				// "regr_slope",
				// "regr_sxx",
				// "regr_sxy",
				// "regr_syy",
				// "reindex", // non-reserved
				// "relative", // non-reserved
				// "release", // non-reserved
				// "rename", // non-reserved
				// "repeatable", // non-reserved
				// "replace", // non-reserved
				// "replica", // non-reserved
				// "requiring",
				// "reset", // non-reserved
				// "respect",
				// "restart", // non-reserved
				// "restore",
				// "restrict", // non-reserved
				// "result",
				// "return", // non-reserved
				// "returned_cardinality",
				// "returned_length",
				// "returned_​octet_​length",
				// "returned_sqlstate",
				"returning", // reserved, requires as
				// "returns", // non-reserved
				// "revoke", // non-reserved
				"right", // reserved (can be function or type)
				// "role", // non-reserved
				// "rollback", // non-reserved
				// "rollup", // non-reserved
				// "routine", // non-reserved
				// "routines", // non-reserved
				// "routine_catalog",
				// "routine_name",
				// "routine_schema",
				// "row", // non-reserved (cannot be function or type)
				// "rows", // non-reserved
				// "row_count",
				// "row_number",
				// "rule", // non-reserved
				// "running",
				// "savepoint", // non-reserved
				// "scalar",
				// "scale",
				// "schema", // non-reserved
				// "schemas", // non-reserved
				// "schema_name",
				// "scope",
				// "scope_catalog",
				// "scope_name",
				// "scope_schema",
				// "scroll", // non-reserved
				// "search", // non-reserved
				"second", // non-reserved, requires as
				// "section",
				// "security", // non-reserved
				// "seek",
				"select", // reserved
				// "selective",
				// "self",
				// "semantics",
				// "sensitive",
				// "sequence", // non-reserved
				// "sequences", // non-reserved
				// "serializable", // non-reserved
				// "server", // non-reserved
				// "server_name",
				// "session", // non-reserved
				"session_user", // reserved
				// "set", // non-reserved
				// "setof", // non-reserved (cannot be function or type)
				// "sets", // non-reserved
				// "share", // non-reserved
				// "show", // non-reserved
				"similar", // reserved (can be function or type)
				// "simple", // non-reserved
				// "sin",
				// "sinh",
				// "size",
				// "skip", // non-reserved
				// "smallint", // non-reserved (cannot be function or type)
				// "snapshot", // non-reserved
				"some", // reserved
				// "sort_direction",
				// "source",
				// "space",
				// "specific",
				// "specifictype",
				// "specific_name",
				// "sql", // non-reserved
				// "sqlcode",
				// "sqlerror",
				// "sqlexception",
				// "sqlstate",
				// "sqlwarning",
				// "sqrt",
				// "stable", // non-reserved
				// "standalone", // non-reserved
				// "start", // non-reserved
				// "state",
				// "statement", // non-reserved
				// "static",
				// "statistics", // non-reserved
				// "stddev_pop",
				// "stddev_samp",
				// "stdin", // non-reserved
				// "stdout", // non-reserved
				// "storage", // non-reserved
				// "stored", // non-reserved
				// "strict", // non-reserved
				// "string",
				// "strip", // non-reserved
				// "structure",
				// "style",
				// "subclass_origin",
				// "submultiset",
				// "subscription", // non-reserved
				// "subset",
				// "substring", // non-reserved (cannot be function or type)
				// "substring_regex",
				// "succeeds",
				// "sum",
				// "support", // non-reserved
				"symmetric", // reserved
				// "sysid", // non-reserved
				// "system", // non-reserved
				// "system_time",
				// "system_user",
				// "t",
				"table", // reserved
				// "tables", // non-reserved
				"tablesample", // reserved (can be function or type)
				// "tablespace", // non-reserved
				// "table_name",
				// "tan",
				// "tanh",
				// "temp", // non-reserved
				// "template", // non-reserved
				// "temporary", // non-reserved
				// "text", // non-reserved
				"then", // reserved
				// "through",
				// "ties", // non-reserved
				// "time", // non-reserved (cannot be function or type)
				// "timestamp", // non-reserved (cannot be function or type)
				// "timezone_hour",
				// "timezone_minute",
				"to", // reserved, requires as
				// "token",
				// "top_level_count",
				"trailing", // reserved
				// "transaction", // non-reserved
				// "transactions_​committed",
				// "transactions_​rolled_​back",
				// "transaction_active",
				// "transform", // non-reserved
				// "transforms",
				// "translate",
				// "translate_regex",
				// "translation",
				// "treat", // non-reserved (cannot be function or type)
				// "trigger", // non-reserved
				// "trigger_catalog",
				// "trigger_name",
				// "trigger_schema",
				// "trim", // non-reserved (cannot be function or type)
				// "trim_array",
				"true", // reserved
				// "truncate", // non-reserved
				// "trusted", // non-reserved
				// "type", // non-reserved
				// "types", // non-reserved
				// "uescape", // non-reserved
				// "unbounded", // non-reserved
				// "uncommitted", // non-reserved
				// "unconditional",
				// "under",
				// "unencrypted", // non-reserved
				"union", // reserved, requires as
				"unique", // reserved
				// "unknown", // non-reserved
				// "unlink",
				// "unlisten", // non-reserved
				// "unlogged", // non-reserved
				// "unmatched",
				// "unnamed",
				// "unnest",
				// "until", // non-reserved
				// "untyped",
				// "update", // non-reserved
				// "upper",
				// "uri",
				// "usage",
				"user", // reserved
				// "user_​defined_​type_​catalog",
				// "user_​defined_​type_​code",
				// "user_​defined_​type_​name",
				// "user_​defined_​type_​schema",
				"using", // reserved
				// "utf16",
				// "utf32",
				// "utf8",
				// "vacuum", // non-reserved
				// "valid", // non-reserved
				// "validate", // non-reserved
				// "validator", // non-reserved
				// "value", // non-reserved
				// "values", // non-reserved (cannot be function or type)
				// "value_of",
				// "varbinary",
				// "varchar", // non-reserved (cannot be function or type)
				"variadic", // reserved
				"varying", // non-reserved, requires as
				// "var_pop",
				// "var_samp",
				"verbose", // reserved (can be function or type)
				// "version", // non-reserved
				// "versioning",
				// "view", // non-reserved
				// "views", // non-reserved
				// "volatile", // non-reserved
				"when", // reserved
				// "whenever",
				"where", // reserved, requires as
				// "whitespace", // non-reserved
				// "width_bucket",
				"window", // reserved, requires as
				"with", // reserved, requires as
				"within", // non-reserved, requires as
				"without", // non-reserved, requires as
				// "work", // non-reserved
				// "wrapper", // non-reserved
				// "write", // non-reserved
				// "xml", // non-reserved
				// "xmlagg",
				// "xmlattributes", // non-reserved (cannot be function or type)
				// "xmlbinary",
				// "xmlcast",
				// "xmlcomment",
				// "xmlconcat", // non-reserved (cannot be function or type)
				// "xmldeclaration",
				// "xmldocument",
				// "xmlelement", // non-reserved (cannot be function or type)
				// "xmlexists", // non-reserved (cannot be function or type)
				// "xmlforest", // non-reserved (cannot be function or type)
				// "xmliterate",
				// "xmlnamespaces", // non-reserved (cannot be function or type)
				// "xmlparse", // non-reserved (cannot be function or type)
				// "xmlpi", // non-reserved (cannot be function or type)
				// "xmlquery",
				// "xmlroot", // non-reserved (cannot be function or type)
				// "xmlschema",
				// "xmlserialize", // non-reserved (cannot be function or type)
				// "xmltable", // non-reserved (cannot be function or type)
				// "xmltext",
				// "xmlvalidate",
				"year", // non-reserved, requires as
				// "yes", // non-reserved
				"zone" // non-reserved
			};
		}
	}
}
