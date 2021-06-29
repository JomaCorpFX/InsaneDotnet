using Insane.EntityFramework.MySql.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure.Internal;
using Pomelo.EntityFrameworkCore.MySql.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Insane.EntityFramework.MySql.Migrations
{
    public class CustomMySqlMigrationsSqlGenerator : MySqlMigrationsSqlGenerator
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "EF1001:Internal EF Core API usage.", Justification = "<Pending>")]
        public CustomMySqlMigrationsSqlGenerator(
            MigrationsSqlGeneratorDependencies dependencies,
            IRelationalAnnotationProvider annotationProvider,
            IMySqlOptions options)
            : base(dependencies, annotationProvider, options)
        {
        }


        protected override void Generate(
            CreateTableOperation operation,
            IModel model,
            MigrationCommandListBuilder builder,
            bool terminate = true)
        {
            base.Generate(operation, model, builder, terminate: false);
            var autoIncrementValue = operation[CustomMySqlAnnotationProvider.AutoincrementAnnotation];
            if (autoIncrementValue is not null && (autoIncrementValue.GetType().Equals(typeof(int)) || autoIncrementValue.GetType().Equals(typeof(long))))
            {
                builder.Append($" AUTO_INCREMENT {autoIncrementValue.ToString()}");
            }

            if (terminate)
            {
                builder.AppendLine(Dependencies.SqlGenerationHelper.StatementTerminator);
                EndStatement(builder);
            }
        }

        protected override void Generate(
            AlterTableOperation operation,
            IModel model,
            MigrationCommandListBuilder builder)
        {
            base.Generate(operation, model, builder);
            var autoIncrementValue = operation[CustomMySqlAnnotationProvider.AutoincrementAnnotation];

            if (autoIncrementValue is not null && (autoIncrementValue.GetType().Equals(typeof(int)) | autoIncrementValue.GetType().Equals(typeof(long))))
            {
                builder.Append("ALTER TABLE ")
                    .Append(Dependencies.SqlGenerationHelper.DelimitIdentifier(operation.Name, operation.Schema))
                    .Append(" AUTO_INCREMENT ")
                    .Append(autoIncrementValue.ToString());

                builder.AppendLine(Dependencies.SqlGenerationHelper.StatementTerminator);
                EndStatement(builder);
            }
        }
    }
}
