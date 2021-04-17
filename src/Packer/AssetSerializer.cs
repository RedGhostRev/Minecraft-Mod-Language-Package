﻿//using Serilog;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Text.Json;
//using Packer.Models;

//namespace Packer
//{
//    static class AssetSerializer
//    {
//        public static string Serialize(Dictionary<string, string> assetMap, FileCategory category)
//        {
//            return category switch
//            {
//                FileCategory.JsonTranslationFormat => JsonSerializer.Serialize<Dictionary<string, string>>(assetMap, new JsonSerializerOptions
//                {
//                    WriteIndented = true
//                }),
//                FileCategory.langTranslationFormat => SerializeFromLang(assetMap),
//                _ => null // 其实不应该执行到这个地方
//            };
//        }

//        static string SerializeFromLang(Dictionary<string, string> assetMap)
//        {
//            var sb = new StringBuilder();
//            foreach (var pair in assetMap)
//            {
//                sb.AppendJoin('=', pair.Key, pair.Value);
//                sb.Append(Environment.NewLine);
//            }
//            return sb.ToString();
//        }

//        public static Dictionary<string, string> Deserialize(string content, string extension)
//        {
//            return extension switch
//            {
//                ".json" => JsonSerializer.Deserialize<Dictionary<string, string>>(content), // 直接有的算法
//                ".lang" => DeserializeFromLang(content),
//                _ => null // 其实不应该执行到这个地方
//            };
//        }

//        static Dictionary<string, string> DeserializeFromLang(string content)
//        {
//            // 下面的 Verbose 仅供调试，不会在 log 里出现
//            // .lang的格式真的乱...
//            Log.Verbose("开始反序列化 .lang 文件");
//            // #PARSE_ESCAPE就算了吧
//            var result = new Dictionary<string, string>();
//            var isInComment = false; // 处理多行注释
//            new List<string>(content.Split(Environment.NewLine,
//                                           StringSplitOptions.RemoveEmptyEntries))
//                .ForEach(line =>
//                {
//                    var isSingleLineComment = false;
//                    new List<string> { "//", "#" }.ForEach(_ => { isSingleLineComment = line.StartsWith(_); });
//                    if (isSingleLineComment)
//                    {
//                        Log.Verbose("跳过了单行注释：{0}", line);
//                    }
//                    else if (isInComment) // 多行注释内
//                    {
//                        Log.Verbose("{0}", line);
//                        if (line.Trim()
//                                   .EndsWith("*/"))
//                        {
//                            isInComment = false;  // 跳出注释
//                        }
//                    }
//                    else if (line.StartsWith("/*")) // 开始多行注释
//                    {
//                        Log.Verbose("跳过了多行注释：{0}", line);
//                    }
//                    else // 真正的条目
//                    {
//                        Log.Verbose("添加对应映射：{0}", line);
//                        var spiltPosition = line.IndexOf('=');
//                        result.Add(line[..(spiltPosition)], line[(spiltPosition + 1)..]);
//                    }
//                }
//            );
//            Log.Verbose("反序列化完成");
//            return result;
//        }

//    }
//}
