#region Copyright (c) 2006-2014 nHydrate.org, All Rights Reserved
// -------------------------------------------------------------------------- *
//                           NHYDRATE.ORG                                     *
//              Copyright (c) 2006-2014 All Rights reserved                   *
//                                                                            *
//                                                                            *
// Permission is hereby granted, free of charge, to any person obtaining a    *
// copy of this software and associated documentation files (the "Software"), *
// to deal in the Software without restriction, including without limitation  *
// the rights to use, copy, modify, merge, publish, distribute, sublicense,   *
// and/or sell copies of the Software, and to permit persons to whom the      *
// Software is furnished to do so, subject to the following conditions:       *
//                                                                            *
// The above copyright notice and this permission notice shall be included    *
// in all copies or substantial portions of the Software.                     *
//                                                                            *
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,            *
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES            *
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.  *
// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY       *
// CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,       *
// TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE          *
// SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.                     *
// -------------------------------------------------------------------------- *
#endregion
using System;
using System.Linq;
using nHydrate.Generator.Common.GeneratorFramework;
using nHydrate.Generator.Models;
using System.Text;
using nHydrate.Generator.Common.Util;
using System.Collections.Generic;

namespace nHydrate.Generator.EFCodeFirst.Generators.ContextExtensions
{
    public class ContextExtensionsGeneratedTemplate : EFCodeFirstBaseTemplate
    {
        private StringBuilder sb = new StringBuilder();

        public ContextExtensionsGeneratedTemplate(ModelRoot model)
            : base(model)
        {
        }

        #region BaseClassTemplate overrides
        public override string FileName
        {
            get { return _model.ProjectName + "EntitiesExtensions.Generated.cs"; }
        }

        public string ParentItemName
        {
            get { return _model.ProjectName + "EntitiesExtensions.cs"; }
        }

        public override string FileContent
        {
            get
            {
                GenerateContent();
                return sb.ToString();
            }
        }
        #endregion

        #region GenerateContent

        private void GenerateContent()
        {
            try
            {
                nHydrate.Generator.GenerationHelper.AppendFileGeneatedMessageInCode(sb);
                nHydrate.Generator.GenerationHelper.AppendCopyrightInCode(sb, _model);
                this.AppendUsingStatements();
                sb.AppendLine("namespace " + this.GetLocalNamespace());
                sb.AppendLine("{");
                this.AppendExtensions();
                sb.AppendLine("}");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void AppendUsingStatements()
        {
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Linq;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using " + this.GetLocalNamespace() + ".Entity;");
            sb.AppendLine("using System.Linq.Expressions;");
            sb.AppendLine();
        }

        private void AppendExtensions()
        {
            sb.AppendLine("	#region " + _model.ProjectName + "EntitiesExtensions");
            sb.AppendLine();
            sb.AppendLine("	/// <summary>");
            sb.AppendLine("	/// Extension methods for this library");
            sb.AppendLine("	/// </summary>");
            sb.AppendLine("	[System.CodeDom.Compiler.GeneratedCode(\"nHydrateModelGenerator\", \"" + _model.ModelToolVersion + "\")]");
            sb.AppendLine("	public static partial class " + _model.ProjectName + "EntitiesExtensions");
            sb.AppendLine("	{");
            sb.AppendLine("		internal static ContextStartup AsAdmin(this ContextStartup o)");
            sb.AppendLine("		{");
            sb.AppendLine("			var q = (ContextStartup)o.Clone();");
            sb.AppendLine("			q.IsAdmin = true;");
            sb.AppendLine("			return q;");
            sb.AppendLine("		}");
            sb.AppendLine();

            #region Include Extension Methods
            //Add an strongly-typed extension for "Include" method
            sb.AppendLine("		#region Include Extension Methods");
            sb.AppendLine();

            sb.AppendLine("		private static System.Data.Entity.Infrastructure.DbQuery<T> GetInclude<T, R>(this System.Data.Entity.Infrastructure.DbQuery<T> item, Expression<Func<R, " + this.GetLocalNamespace() + ".IContextInclude>> query)");
            sb.AppendLine("			where T : BaseEntity");
            sb.AppendLine("		{");
            sb.AppendLine("			var strings = new List<string>(query.Body.ToString().Split('.'));");
            sb.AppendLine("			strings.RemoveAt(0);");
            sb.AppendLine("			var compoundString = string.Empty;");
            sb.AppendLine("			foreach (var s in strings)");
            sb.AppendLine("			{");
            sb.AppendLine("				if (!string.IsNullOrEmpty(compoundString)) compoundString += \".\";");
            sb.AppendLine("				compoundString += s;");
            sb.AppendLine("				item = item.Include(compoundString);");
            sb.AppendLine("			}");
            sb.AppendLine("			return item;");
            sb.AppendLine("		}");
            sb.AppendLine();
            sb.AppendLine("		private static IQueryable<T> GetInclude<T, R>(this IQueryable<T> item, Expression<Func<R, " + this.GetLocalNamespace() + ".IContextInclude>> query)");
            sb.AppendLine("		{");
            sb.AppendLine("			var tempItem = item as System.Data.Entity.Core.Objects.ObjectQuery<T>;");
            sb.AppendLine("			if (tempItem == null) return item;");
            sb.AppendLine("			var strings = new List<string>(query.Body.ToString().Split('.'));");
            sb.AppendLine("			strings.RemoveAt(0);");
            sb.AppendLine("			var compoundString = string.Empty;");
            sb.AppendLine("			foreach (var s in strings)");
            sb.AppendLine("			{");
            sb.AppendLine("				if (!string.IsNullOrEmpty(compoundString)) compoundString += \".\";");
            sb.AppendLine("				compoundString += s;");
            sb.AppendLine("				tempItem = tempItem.Include(compoundString);");
            sb.AppendLine("			}");
            sb.AppendLine("			return tempItem;");
            sb.AppendLine("		}");
            sb.AppendLine();

            ////Add one for DbQuery
            //sb.AppendLine("		/// <summary>");
            //sb.AppendLine("		/// Specifies the related objects to include in the query results.");
            //sb.AppendLine("		/// </summary>");
            //sb.AppendLine("		/// <param name=\"item\">Related object to return in query results</param>");
            //sb.AppendLine("		/// <param name=\"query\">The LINQ expresssion that maps an include path</param>");
            //sb.AppendLine("		public static System.Data.Entity.Infrastructure.DbQuery<T> Include<T, R>(this System.Data.Entity.Infrastructure.DbQuery<T> item, Expression<Func<T, R>> query)");
            //sb.AppendLine("		{");
            //sb.AppendLine("			var strings = new List<string>(query.Body.ToString().Split('.'));");
            //sb.AppendLine("			strings.RemoveAt(0);");
            //sb.AppendLine("			var compoundString = string.Empty;");
            //sb.AppendLine("			foreach (var s in strings)");
            //sb.AppendLine("			{");
            //sb.AppendLine("				if (!string.IsNullOrEmpty(compoundString)) compoundString += \".\";");
            //sb.AppendLine("				compoundString += s;");
            //sb.AppendLine("				item = item.Include(compoundString);");
            //sb.AppendLine("			}");
            //sb.AppendLine("			return item;");
            //sb.AppendLine("		}");
            //sb.AppendLine();
            //sb.AppendLine("		/// <summary>");
            //sb.AppendLine("		/// Specifies the related objects to include in the query results.");
            //sb.AppendLine("		/// </summary>");
            //sb.AppendLine("		/// <param name=\"item\">Related object to return in query results</param>");
            //sb.AppendLine("		/// <param name=\"query\">The LINQ expresssion that maps an include path</param>");
            //sb.AppendLine("		public static System.Data.Entity.Infrastructure.DbQuery<T> Include<T, R>(this System.Data.Entity.Infrastructure.DbQuery<T> item, Expression<Func<T, ICollection<R>>> query)");
            //sb.AppendLine("		{");
            //sb.AppendLine("			var strings = new List<string>(query.Body.ToString().Split('.'));");
            //sb.AppendLine("			strings.RemoveAt(0);");
            //sb.AppendLine("			var compoundString = string.Empty;");
            //sb.AppendLine("			foreach (var s in strings)");
            //sb.AppendLine("			{");
            //sb.AppendLine("				if (!string.IsNullOrEmpty(compoundString)) compoundString += \".\";");
            //sb.AppendLine("				compoundString += s;");
            //sb.AppendLine("				item = item.Include(compoundString);");
            //sb.AppendLine("			}");
            //sb.AppendLine("			return item;");
            //sb.AppendLine("		}");
            //sb.AppendLine();

            ////Now add one for IQueryable
            //sb.AppendLine("		/// <summary>");
            //sb.AppendLine("		/// Specifies the related objects to include in the query results.");
            //sb.AppendLine("		/// </summary>");
            //sb.AppendLine("		/// <param name=\"item\">Related object to return in query results</param>");
            //sb.AppendLine("		/// <param name=\"query\">The LINQ expresssion that maps an include path</param>");
            //sb.AppendLine("		public static IQueryable<T> Include<T, R>(this IQueryable<T> item, Expression<Func<T, R>> query)");
            //sb.AppendLine("		{");
            //sb.AppendLine("			var tempItem = item as System.Data.Entity.Core.Objects.ObjectQuery<T>;");
            //sb.AppendLine("			if (tempItem == null) return item;");
            //sb.AppendLine("			var strings = new List<string>(query.Body.ToString().Split('.'));");
            //sb.AppendLine("			strings.RemoveAt(0);");
            //sb.AppendLine("			var compoundString = string.Empty;");
            //sb.AppendLine("			foreach (var s in strings)");
            //sb.AppendLine("			{");
            //sb.AppendLine("				if (!string.IsNullOrEmpty(compoundString)) compoundString += \".\";");
            //sb.AppendLine("				compoundString += s;");
            //sb.AppendLine("				tempItem = tempItem.Include(compoundString);");
            //sb.AppendLine("			}");
            //sb.AppendLine("			return tempItem;");
            //sb.AppendLine("		}");
            //sb.AppendLine();
            //sb.AppendLine("		/// <summary>");
            //sb.AppendLine("		/// Specifies the related objects to include in the query results.");
            //sb.AppendLine("		/// </summary>");
            //sb.AppendLine("		/// <param name=\"item\">Related object to return in query results</param>");
            //sb.AppendLine("		/// <param name=\"query\">The LINQ expresssion that maps an include path</param>");
            //sb.AppendLine("		public static IQueryable<T> Include<T, R>(this IQueryable<T> item, Expression<Func<T, ICollection<R>>> query)");
            //sb.AppendLine("		{");
            //sb.AppendLine("			var tempItem = item as System.Data.Entity.Core.Objects.ObjectQuery<T>;");
            //sb.AppendLine("			if (tempItem == null) return item;");
            //sb.AppendLine("			var strings = new List<string>(query.Body.ToString().Split('.'));");
            //sb.AppendLine("			strings.RemoveAt(0);");
            //sb.AppendLine("			var compoundString = string.Empty;");
            //sb.AppendLine("			foreach (var s in strings)");
            //sb.AppendLine("			{");
            //sb.AppendLine("				if (!string.IsNullOrEmpty(compoundString)) compoundString += \".\";");
            //sb.AppendLine("				compoundString += s;");
            //sb.AppendLine("				tempItem = tempItem.Include(compoundString);");
            //sb.AppendLine("			}");
            //sb.AppendLine("			return tempItem;");
            //sb.AppendLine("		}");
            //sb.AppendLine();

            foreach (var table in _model.Database.Tables.Where(x => x.Generated && !x.AssociativeTable && (x.TypedTable == TypedTableConstants.None)).OrderBy(x => x.PascalName))
            {
                //Build relation list
                var relationList1 = table.GetRelationsFullHierarchy().Where(x =>
                    x.ParentTableRef.Object == table &&
                    !(x.ChildTableRef.Object as Table).IsInheritedFrom(x.ParentTableRef.Object as Table)
                    );

                var relationList2 = table.GetRelationsWhereChild().Where(x =>
                    x.ChildTableRef.Object == table &&
                    !(x.ChildTableRef.Object as Table).IsInheritedFrom(x.ParentTableRef.Object as Table)
                    );

                var relationList = new List<Relation>();
                relationList.AddRange(relationList1);
                relationList.AddRange(relationList2);

                //Generate an extension if there are relations for this table
                if (relationList.Count() != 0 && table.TypedTable != TypedTableConstants.EnumOnly)
                {
                    ////Add one for DbQuery
                    //sb.AppendLine("		/// <summary>");
                    //sb.AppendLine("		/// Specifies the related objects to include in the query results.");
                    //sb.AppendLine("		/// </summary>");
                    //sb.AppendLine("		/// <param name=\"item\">Related object to return in query results</param>");
                    //sb.AppendLine("		/// <param name=\"query\">The LINQ expresssion that maps an include path</param>");
                    //sb.AppendLine("		public static System.Data.Entity.Infrastructure.DbQuery<" + this.GetLocalNamespace() + ".Entity." + table.PascalName + "> Include(this System.Data.Entity.Infrastructure.DbQuery<" + this.GetLocalNamespace() + ".Entity." + table.PascalName + "> item, Expression<Func<" + this.GetLocalNamespace() + "." + table.PascalName + "Include, " + this.GetLocalNamespace() + ".IContextInclude>> query)");
                    //sb.AppendLine("		{");
                    //sb.AppendLine("			var strings = new List<string>(query.Body.ToString().Split('.'));");
                    //sb.AppendLine("			strings.RemoveAt(0);");
                    //sb.AppendLine("			var compoundString = string.Empty;");
                    //sb.AppendLine("			foreach (var s in strings)");
                    //sb.AppendLine("			{");
                    //sb.AppendLine("				if (!string.IsNullOrEmpty(compoundString)) compoundString += \".\";");
                    //sb.AppendLine("				compoundString += s;");
                    //sb.AppendLine("				item = item.Include(compoundString);");
                    //sb.AppendLine("			}");
                    //sb.AppendLine("			return item;");
                    //sb.AppendLine("		}");
                    //sb.AppendLine();

                    //Add one for DbQuery
                    sb.AppendLine("		/// <summary>");
                    sb.AppendLine("		/// Specifies the related objects to include in the query results.");
                    sb.AppendLine("		/// </summary>");
                    sb.AppendLine("		/// <param name=\"item\">Related object to return in query results</param>");
                    sb.AppendLine("		/// <param name=\"query\">The LINQ expresssion that maps an include path</param>");
                    sb.AppendLine("		public static System.Data.Entity.Infrastructure.DbQuery<" + this.GetLocalNamespace() + ".Entity." + table.PascalName + "> Include(this System.Data.Entity.Infrastructure.DbQuery<" + this.GetLocalNamespace() + ".Entity." + table.PascalName + "> item, Expression<Func<" + this.GetLocalNamespace() + "." + table.PascalName + "Include, " + this.GetLocalNamespace() + ".IContextInclude>> query)");
                    sb.AppendLine("		{");
                    sb.AppendLine("			return GetInclude<" + this.GetLocalNamespace() + ".Entity." + table.PascalName + ", " + this.GetLocalNamespace() + "." + table.PascalName + "Include>(item, query);");
                    sb.AppendLine("		}");
                    sb.AppendLine();

                    ////Now add one for IQueryable
                    //sb.AppendLine("		/// <summary>");
                    //sb.AppendLine("		/// Specifies the related objects to include in the query results.");
                    //sb.AppendLine("		/// </summary>");
                    //sb.AppendLine("		/// <param name=\"item\">Related object to return in query results</param>");
                    //sb.AppendLine("		/// <param name=\"query\">The LINQ expresssion that maps an include path</param>");
                    //sb.AppendLine("		public static IQueryable<" + this.GetLocalNamespace() + ".Entity." + table.PascalName + "> Include(this IQueryable<" + this.GetLocalNamespace() + ".Entity." + table.PascalName + "> item, Expression<Func<" + this.GetLocalNamespace() + "." + table.PascalName + "Include, " + this.GetLocalNamespace() + ".IContextInclude>> query)");
                    //sb.AppendLine("		{");
                    //sb.AppendLine("			var tempItem = item as System.Data.Entity.Core.Objects.ObjectQuery<" + this.GetLocalNamespace() + ".Entity." + table.PascalName + ">;");
                    //sb.AppendLine("			if (tempItem == null) return item;");
                    //sb.AppendLine("			var strings = new List<string>(query.Body.ToString().Split('.'));");
                    //sb.AppendLine("			strings.RemoveAt(0);");
                    //sb.AppendLine("			var compoundString = string.Empty;");
                    //sb.AppendLine("			foreach (var s in strings)");
                    //sb.AppendLine("			{");
                    //sb.AppendLine("				if (!string.IsNullOrEmpty(compoundString)) compoundString += \".\";");
                    //sb.AppendLine("				compoundString += s;");
                    //sb.AppendLine("				tempItem = tempItem.Include(compoundString);");
                    //sb.AppendLine("			}");
                    //sb.AppendLine("			return tempItem;");
                    //sb.AppendLine("		}");
                    //sb.AppendLine();

                    //Now add one for IQueryable
                    sb.AppendLine("		/// <summary>");
                    sb.AppendLine("		/// Specifies the related objects to include in the query results.");
                    sb.AppendLine("		/// </summary>");
                    sb.AppendLine("		/// <param name=\"item\">Related object to return in query results</param>");
                    sb.AppendLine("		/// <param name=\"query\">The LINQ expresssion that maps an include path</param>");
                    sb.AppendLine("		public static IQueryable<" + this.GetLocalNamespace() + ".Entity." + table.PascalName + "> Include(this IQueryable<" + this.GetLocalNamespace() + ".Entity." + table.PascalName + "> item, Expression<Func<" + this.GetLocalNamespace() + "." + table.PascalName + "Include, " + this.GetLocalNamespace() + ".IContextInclude>> query)");
                    sb.AppendLine("		{");
                    sb.AppendLine("			return GetInclude<" + this.GetLocalNamespace() + ".Entity." + table.PascalName + ", " + this.GetLocalNamespace() + "." + table.PascalName + "Include>(item, query);");
                    sb.AppendLine("		}");
                    sb.AppendLine();

                }
            }
            sb.AppendLine("		#endregion");
            sb.AppendLine();
            #endregion

            #region GetFieldType
            sb.AppendLine("		#region GetFieldType Extension Method");
            sb.AppendLine();
            sb.AppendLine("		/// <summary>");
            sb.AppendLine("		/// Get the system type of a field of one of the contained context objects");
            sb.AppendLine("		/// </summary>");
            sb.AppendLine("		public static System.Type GetFieldType(this " + this.GetLocalNamespace() + "." + _model.ProjectName + "Entities context, Enum field)");
            sb.AppendLine("		{");
            foreach (var table in _model.Database.Tables.Where(x => x.Generated && !x.AssociativeTable && (x.TypedTable != TypedTableConstants.EnumOnly)).OrderBy(x => x.PascalName))
            {
                sb.AppendLine("			if (field is " + this.GetLocalNamespace() + ".Entity." + table.PascalName + ".FieldNameConstants)");
                sb.AppendLine("				return " + GetLocalNamespace() + ".Entity." + table.PascalName + ".GetFieldType((" + this.GetLocalNamespace() + ".Entity." + table.PascalName + ".FieldNameConstants)field);");
            }
            sb.AppendLine("			throw new Exception(\"Unknown field type!\");");
            sb.AppendLine("		}");
            sb.AppendLine();
            sb.AppendLine("		#endregion");
            sb.AppendLine();
            #endregion

            #region Metadata Extension Methods
            sb.AppendLine("		#region Metadata Extension Methods");
            sb.AppendLine();

            //Main one for base IReadOnlyBusinessObject object
            sb.AppendLine("		/// <summary>");
            sb.AppendLine("		/// Creates and returns a metadata object for an entity type");
            sb.AppendLine("		/// </summary>");
            sb.AppendLine("		/// <param name=\"entity\">The source class</param>");
            sb.AppendLine("		/// <returns>A metadata object for the entity types in this assembly</returns>");
            sb.AppendLine("		public static " + this.GetLocalNamespace() + ".IMetadata GetMetaData(this " + this.GetLocalNamespace() + ".IReadOnlyBusinessObject entity)");
            sb.AppendLine("		{");
            sb.AppendLine("			var a = entity.GetType().GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.MetadataTypeAttribute), true).FirstOrDefault();");
            sb.AppendLine("			if (a == null) return null;");
            sb.AppendLine("			var t = ((System.ComponentModel.DataAnnotations.MetadataTypeAttribute)a).MetadataClassType;");
            sb.AppendLine("			if (t == null) return null;");
            sb.AppendLine("			return Activator.CreateInstance(t) as " + this.GetLocalNamespace() + ".IMetadata;");
            sb.AppendLine("		}");
            sb.AppendLine();

            sb.AppendLine("		#endregion");
            sb.AppendLine();
            #endregion

            #region GetEntityType

            sb.AppendLine("		#region GetEntityType");
            sb.AppendLine();
            sb.AppendLine("		/// <summary>");
            sb.AppendLine("		/// Determines the entity from one of its fields");
            sb.AppendLine("		/// </summary>");
            sb.AppendLine("		public static System.Type GetEntityType(EntityMappingConstants entityType)");
            sb.AppendLine("		{");
            sb.AppendLine("			switch (entityType)");
            sb.AppendLine("			{");
            foreach (var table in _model.Database.Tables.Where(x => x.Generated && !x.AssociativeTable && (x.TypedTable != TypedTableConstants.EnumOnly)).OrderBy(x => x.PascalName))
                sb.AppendLine("				case EntityMappingConstants." + table.PascalName + ": return typeof(" + this.GetLocalNamespace() + ".Entity." + table.PascalName + ");");
            sb.AppendLine("			}");
            sb.AppendLine("			throw new Exception(\"Unknown entity type!\");");
            sb.AppendLine("		}");
            sb.AppendLine();
            sb.AppendLine("		#endregion");
            sb.AppendLine();

            #endregion

            #region GetValue Methods
            sb.AppendLine("		#region GetValue Methods");
            sb.AppendLine();

            foreach (var table in _model.Database.Tables.Where(x => x.Generated && !x.AssociativeTable && (x.TypedTable != TypedTableConstants.EnumOnly)).OrderBy(x => x.PascalName))
            {
                //GetValue by lambda
                sb.AppendLine("		/// <summary>");
                sb.AppendLine("		/// Gets the value of one of this object's properties.");
                sb.AppendLine("		/// </summary>");
                sb.AppendLine("		/// <typeparam name=\"T\">The type of value to retrieve</typeparam>");
                sb.AppendLine("		/// <param name=\"item\">The item from which to pull the value.</param>");
                sb.AppendLine("		/// <param name=\"selector\">The field to retrieve</param>");
                sb.AppendLine("		/// <returns></returns>");
                sb.AppendLine("		public static T GetValue<T>(this " + this.GetLocalNamespace() + ".Entity." + table.PascalName + " item, System.Linq.Expressions.Expression<System.Func<" + this.GetLocalNamespace() + ".Entity." + table.PascalName + ", T>> selector)");
                sb.AppendLine("		{");
                sb.AppendLine("			var b = selector.Body.ToString();");
                sb.AppendLine("			var arr = b.Split('.');");
                sb.AppendLine("			if (arr.Length != 2) throw new System.Exception(\"Invalid selector\");");
                sb.AppendLine("			var tn = arr.Last();");
                sb.AppendLine("			var te = (" + this.GetLocalNamespace() + ".Entity." + table.PascalName + ".FieldNameConstants)Enum.Parse(typeof(" + this.GetLocalNamespace() + ".Entity." + table.PascalName + ".FieldNameConstants), tn, true);");
                sb.AppendLine("			return item.GetValue<T>(te, default(T));");
                sb.AppendLine("		}");
                sb.AppendLine();

                //GetValue by lambda with default
                sb.AppendLine("		/// <summary>");
                sb.AppendLine("		/// Gets the value of one of this object's properties.");
                sb.AppendLine("		/// </summary>");
                sb.AppendLine("		/// <typeparam name=\"T\">The type of value to retrieve</typeparam>");
                sb.AppendLine("		/// <param name=\"item\">The item from which to pull the value.</param>");
                sb.AppendLine("		/// <param name=\"selector\">The field to retrieve</param>");
                sb.AppendLine("		/// <param name=\"defaultValue\">The default value to return if the specified value is NULL</param>");
                sb.AppendLine("		/// <returns></returns>");
                sb.AppendLine("		public static T GetValue<T>(this " + this.GetLocalNamespace() + ".Entity." + table.PascalName + " item, System.Linq.Expressions.Expression<System.Func<" + this.GetLocalNamespace() + ".Entity." + table.PascalName + ", T>> selector, T defaultValue)");
                sb.AppendLine("		{");
                sb.AppendLine("			var b = selector.Body.ToString();");
                sb.AppendLine("			var arr = b.Split('.');");
                sb.AppendLine("			if (arr.Length != 2) throw new System.Exception(\"Invalid selector\");");
                sb.AppendLine("			var tn = arr.Last();");
                sb.AppendLine("			var te = (" + this.GetLocalNamespace() + ".Entity." + table.PascalName + ".FieldNameConstants)Enum.Parse(typeof(" + this.GetLocalNamespace() + ".Entity." + table.PascalName + ".FieldNameConstants), tn, true);");
                sb.AppendLine("			return item.GetValue<T>(te, defaultValue);");
                sb.AppendLine("		}");
                sb.AppendLine();

                //GetValue by by Enum
                sb.AppendLine("		/// <summary>");
                sb.AppendLine("		/// Gets the value of one of this object's properties.");
                sb.AppendLine("		/// </summary>");
                sb.AppendLine("		/// <typeparam name=\"T\">The type of value to retrieve</typeparam>");
                sb.AppendLine("		/// <param name=\"item\">The item from which to pull the value.</param>");
                sb.AppendLine("		/// <param name=\"field\">The field value to retrieve</param>");
                sb.AppendLine("		/// <returns></returns>");
                sb.AppendLine("		public static T GetValue<T>(this " + this.GetLocalNamespace() + ".Entity." + table.PascalName + " item, " + this.GetLocalNamespace() + ".Entity." + table.PascalName + ".FieldNameConstants field)");
                sb.AppendLine("		{");
                sb.AppendLine("			return item.GetValue<T>(field, default(T));");
                sb.AppendLine("		}");
                sb.AppendLine();

                //GetValue by by Enum with default
                sb.AppendLine("		/// <summary>");
                sb.AppendLine("		/// Gets the value of one of this object's properties.");
                sb.AppendLine("		/// </summary>");
                sb.AppendLine("		/// <typeparam name=\"T\">The type of value to retrieve</typeparam>");
                sb.AppendLine("		/// <param name=\"item\">The item from which to pull the value.</param>");
                sb.AppendLine("		/// <param name=\"field\">The field value to retrieve</param>");
                sb.AppendLine("		/// <param name=\"defaultValue\">The default value to return if the specified value is NULL</param>");
                sb.AppendLine("		/// <returns></returns>");
                sb.AppendLine("		public static T GetValue<T>(this " + this.GetLocalNamespace() + ".Entity." + table.PascalName + " item, " + this.GetLocalNamespace() + ".Entity." + table.PascalName + ".FieldNameConstants field, T defaultValue)");
                sb.AppendLine("		{");
                sb.AppendLine("			var valid = false;");
                sb.AppendLine("			if (typeof(T) == typeof(bool)) valid = true;");
                sb.AppendLine("			else if (typeof(T) == typeof(byte)) valid = true;");
                sb.AppendLine("			else if (typeof(T) == typeof(char)) valid = true;");
                sb.AppendLine("			else if (typeof(T) == typeof(DateTime)) valid = true;");
                sb.AppendLine("			else if (typeof(T) == typeof(decimal)) valid = true;");
                sb.AppendLine("			else if (typeof(T) == typeof(double)) valid = true;");
                sb.AppendLine("			else if (typeof(T) == typeof(int)) valid = true;");
                sb.AppendLine("			else if (typeof(T) == typeof(long)) valid = true;");
                sb.AppendLine("			else if (typeof(T) == typeof(Single)) valid = true;");
                sb.AppendLine("			else if (typeof(T) == typeof(string)) valid = true;");
                sb.AppendLine("			if (!valid)");
                sb.AppendLine("				throw new Exception(\"Cannot convert object to type '\" + typeof(T).ToString() + \"'!\");");
                sb.AppendLine();
                sb.AppendLine("			object o = item.GetValue(field, defaultValue);");
                sb.AppendLine("			if (o == null) return defaultValue;");
                sb.AppendLine();
                sb.AppendLine("			if (o is T)");
                sb.AppendLine("			{");
                sb.AppendLine("				return (T)o;");
                sb.AppendLine("			}");
                sb.AppendLine("			else if (typeof(T) == typeof(bool))");
                sb.AppendLine("			{");
                sb.AppendLine("				return (T)(object)Convert.ToBoolean(o);");
                sb.AppendLine("			}");
                sb.AppendLine("			else if (typeof(T) == typeof(byte))");
                sb.AppendLine("			{");
                sb.AppendLine("				return (T)(object)Convert.ToByte(o);");
                sb.AppendLine("			}");
                sb.AppendLine("			else if (typeof(T) == typeof(char))");
                sb.AppendLine("			{");
                sb.AppendLine("				return (T)(object)Convert.ToChar(o);");
                sb.AppendLine("			}");
                sb.AppendLine("			else if (typeof(T) == typeof(DateTime))");
                sb.AppendLine("			{");
                sb.AppendLine("				return (T)(object)Convert.ToDateTime(o);");
                sb.AppendLine("			}");
                sb.AppendLine("			else if (typeof(T) == typeof(decimal))");
                sb.AppendLine("			{");
                sb.AppendLine("				return (T)(object)Convert.ToDecimal(o);");
                sb.AppendLine("			}");
                sb.AppendLine("			else if (typeof(T) == typeof(double))");
                sb.AppendLine("			{");
                sb.AppendLine("				return (T)(object)Convert.ToDouble(o);");
                sb.AppendLine("			}");
                sb.AppendLine("			else if (typeof(T) == typeof(int))");
                sb.AppendLine("			{");
                sb.AppendLine("				return (T)(object)Convert.ToInt32(o);");
                sb.AppendLine("			}");
                sb.AppendLine("			else if (typeof(T) == typeof(long))");
                sb.AppendLine("			{");
                sb.AppendLine("				return (T)(object)Convert.ToInt64(o);");
                sb.AppendLine("			}");
                sb.AppendLine("			else if (typeof(T) == typeof(Single))");
                sb.AppendLine("			{");
                sb.AppendLine("				return (T)(object)Convert.ToSingle(o);");
                sb.AppendLine("			}");
                sb.AppendLine("			else if (typeof(T) == typeof(string))");
                sb.AppendLine("			{");
                sb.AppendLine("				return (T)(object)Convert.ToString(o);");
                sb.AppendLine("			}");
                sb.AppendLine("			throw new Exception(\"Cannot convert object!\");");
                sb.AppendLine("		}");
                sb.AppendLine();
            }
            sb.AppendLine("		#endregion");
            sb.AppendLine();
            #endregion

            #region SetValue
            sb.AppendLine("		#region SetValue");
            foreach (var table in _model.Database.Tables.Where(x => x.Generated && !x.Immutable && !x.AssociativeTable).OrderBy(x => x.Name))
            {
                sb.AppendLine("		/// <summary>");
                sb.AppendLine("		/// Assigns a value to a field on this object.");
                sb.AppendLine("		/// </summary>");
                sb.AppendLine("		/// <param name=\"item\">The entity to set</param>");
                sb.AppendLine("		/// <param name=\"selector\">The field on the entity to set</param>");
                sb.AppendLine("		/// <param name=\"newValue\">The new value to assign to the field</param>");
                sb.AppendLine("		public static void SetValue<TResult>(this " + this.GetLocalNamespace() + ".Entity." + table.PascalName + " item, System.Linq.Expressions.Expression<System.Func<" + this.GetLocalNamespace() + ".Entity." + table.PascalName + ", TResult>> selector, TResult newValue)");
                sb.AppendLine("		{");
                sb.AppendLine("			SetValue(item, selector, newValue, false);");
                sb.AppendLine("		}");
                sb.AppendLine();

                sb.AppendLine("		/// <summary>");
                sb.AppendLine("		/// Assigns a value to a field on this object.");
                sb.AppendLine("		/// </summary>");
                sb.AppendLine("		/// <param name=\"item\">The entity to set</param>");
                sb.AppendLine("		/// <param name=\"selector\">The field on the entity to set</param>");
                sb.AppendLine("		/// <param name=\"newValue\">The new value to assign to the field</param>");
                sb.AppendLine("		/// <param name=\"fixLength\">Determines if the length should be truncated if too long. When false, an error will be raised if data is too large to be assigned to the field.</param>");
                sb.AppendLine("		public static void SetValue<TResult>(this " + this.GetLocalNamespace() + ".Entity." + table.PascalName + " item, System.Linq.Expressions.Expression<System.Func<" + this.GetLocalNamespace() + ".Entity." + table.PascalName + ", TResult>> selector, TResult newValue, bool fixLength)");
                sb.AppendLine("		{");
                sb.AppendLine("			var b = selector.Body.ToString();");
                sb.AppendLine("			var arr = b.Split('.');");
                sb.AppendLine("			if (arr.Length != 2) throw new System.Exception(\"Invalid selector\");");
                sb.AppendLine("			var tn = arr.Last();");
                sb.AppendLine("			var te = (" + this.GetLocalNamespace() + ".Entity." + table.PascalName + ".FieldNameConstants)Enum.Parse(typeof(" + this.GetLocalNamespace() + ".Entity." + table.PascalName + ".FieldNameConstants), tn, true);");
                sb.AppendLine("			item.SetValue(te, newValue);");
                sb.AppendLine("		}");
                sb.AppendLine();
            }
            sb.AppendLine("		#endregion");
            sb.AppendLine();
            #endregion

            #region ObservableCollection
            sb.AppendLine("		#region ObservableCollection");
            sb.AppendLine("		/// <summary>");
            sb.AppendLine("		/// Returns an observable collection that can bound to UI controls");
            sb.AppendLine("		/// </summary>");
            sb.AppendLine("		public static System.Collections.ObjectModel.ObservableCollection<T> AsObservable<T>(this System.Collections.Generic.IEnumerable<T> list)");
            sb.AppendLine("			where T : " + this.GetLocalNamespace() + ".IReadOnlyBusinessObject");
            sb.AppendLine("		{");
            sb.AppendLine("			var retval = new System.Collections.ObjectModel.ObservableCollection<T>();");
            sb.AppendLine("			foreach (var o in list)");
            sb.AppendLine("				retval.Add(o);");
            sb.AppendLine("			return retval;");
            sb.AppendLine("		}");
            sb.AppendLine("		#endregion");
            sb.AppendLine();
            #endregion

            sb.AppendLine("	}");
            sb.AppendLine();

            #region SequentialId

            sb.AppendLine("	#region SequentialIdGenerator");
            sb.AppendLine();
            sb.AppendLine("	/// <summary>");
            sb.AppendLine("	/// Generates Sequential Guid values that can be used for Sql Server UniqueIdentifiers to improve performance.");
            sb.AppendLine("	/// </summary>");
            sb.AppendLine("	internal class SequentialIdGenerator");
            sb.AppendLine("	{");
            sb.AppendLine("		private readonly object _lock;");
            sb.AppendLine("		private Guid _lastGuid;");
            sb.AppendLine("		// 3 - the least significant byte in Guid ByteArray [for SQL Server ORDER BY clause]");
            sb.AppendLine("		// 10 - the most significant byte in Guid ByteArray [for SQL Server ORDERY BY clause]");
            sb.AppendLine("		private static readonly int[] SqlOrderMap = new int[] { 3, 2, 1, 0, 5, 4, 7, 6, 9, 8, 15, 14, 13, 12, 11, 10 };");
            sb.AppendLine();
            sb.AppendLine("		/// <summary>");
            sb.AppendLine("		/// Creates a new SequentialId class to generate sequential GUID values.");
            sb.AppendLine("		/// </summary>");
            sb.AppendLine("		public SequentialIdGenerator() : this(Guid.NewGuid()) { }");
            sb.AppendLine();
            sb.AppendLine("		/// <summary>");
            sb.AppendLine("		/// Creates a new SequentialId class to generate sequential GUID values.");
            sb.AppendLine("		/// </summary>");
            sb.AppendLine("		/// <param name=\"seed\">Starting seed value.</param>");
            sb.AppendLine("		/// <remarks>You can save the last generated value <see cref=\"LastValue\"/> and then ");
            sb.AppendLine("		/// use this as the new seed value to pick up where you left off.</remarks>");
            sb.AppendLine("		public SequentialIdGenerator(Guid seed)");
            sb.AppendLine("		{");
            sb.AppendLine("			_lock = new object();");
            sb.AppendLine("			_lastGuid = seed;");
            sb.AppendLine("		}");
            sb.AppendLine();
            sb.AppendLine("		/// <summary>");
            sb.AppendLine("		/// Last generated guid value.  If no values have been generated, this will be the seed value.");
            sb.AppendLine("		/// </summary>");
            sb.AppendLine("		public Guid LastValue");
            sb.AppendLine("		{");
            sb.AppendLine("			get {");
            sb.AppendLine("				lock (_lock)");
            sb.AppendLine("				{");
            sb.AppendLine("					return _lastGuid;");
            sb.AppendLine("				}");
            sb.AppendLine("			}");
            sb.AppendLine("			set");
            sb.AppendLine("			{");
            sb.AppendLine("				lock (_lock)");
            sb.AppendLine("				{");
            sb.AppendLine("					_lastGuid = value;");
            sb.AppendLine("				}");
            sb.AppendLine("			}");
            sb.AppendLine("		}");
            sb.AppendLine();
            sb.AppendLine("		/// <summary>");
            sb.AppendLine("		/// Generate a new sequential id.");
            sb.AppendLine("		/// </summary>");
            sb.AppendLine("		/// <returns>New sequential id value.</returns>");
            sb.AppendLine("		public Guid NewId()");
            sb.AppendLine("		{");
            sb.AppendLine("			Guid newId;");
            sb.AppendLine("			lock (_lock)");
            sb.AppendLine("			{");
            sb.AppendLine("				var guidBytes = _lastGuid.ToByteArray();");
            sb.AppendLine("				ReorderToSqlOrder(ref guidBytes);");
            sb.AppendLine("				newId = new Guid(guidBytes);");
            sb.AppendLine("				_lastGuid = newId;");
            sb.AppendLine("			}");
            sb.AppendLine();
            sb.AppendLine("			return newId;");
            sb.AppendLine("		}");
            sb.AppendLine();
            sb.AppendLine("		private static void ReorderToSqlOrder(ref byte[] bytes)");
            sb.AppendLine("		{");
            sb.AppendLine("			foreach (var bytesIndex in SqlOrderMap)");
            sb.AppendLine("			{");
            sb.AppendLine("				bytes[bytesIndex]++;");
            sb.AppendLine("				if (bytes[bytesIndex] != 0)");
            sb.AppendLine("				{");
            sb.AppendLine("					break;");
            sb.AppendLine("				}");
            sb.AppendLine("			}");
            sb.AppendLine("		}");
            sb.AppendLine();
            sb.AppendLine("		/// <summary>");
            sb.AppendLine("		/// IComparer.Compare compatible method to order Guid values the same way as MS Sql Server.");
            sb.AppendLine("		/// </summary>");
            sb.AppendLine("		/// <param name=\"x\">The first guid to compare</param>");
            sb.AppendLine("		/// <param name=\"y\">The second guid to compare</param>");
            sb.AppendLine("		/// <returns><see cref=\"System.Collections.IComparer.Compare\"/></returns>");
            sb.AppendLine("		public static int SqlCompare(Guid x, Guid y)");
            sb.AppendLine("		{");
            sb.AppendLine("			var result = 0;");
            sb.AppendLine("			var index = SqlOrderMap.Length - 1;");
            sb.AppendLine("			var xBytes = x.ToByteArray();");
            sb.AppendLine("			var yBytes = y.ToByteArray();");
            sb.AppendLine();
            sb.AppendLine("			while (result == 0 && index >= 0)");
            sb.AppendLine("			{");
            sb.AppendLine("				result = xBytes[SqlOrderMap[index]].CompareTo(yBytes[SqlOrderMap[index]]);");
            sb.AppendLine("				index--;");
            sb.AppendLine("			}");
            sb.AppendLine("			return result;");
            sb.AppendLine("		}");
            sb.AppendLine("	}");
            sb.AppendLine();
            sb.AppendLine("	#endregion");
            sb.AppendLine();
            #endregion

            sb.AppendLine();
            sb.AppendLine("	#endregion");
            sb.AppendLine();
        }

        #endregion

    }
}