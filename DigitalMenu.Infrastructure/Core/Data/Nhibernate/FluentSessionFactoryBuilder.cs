using DigitalMenu.Infrastructure.Core.Data.Nhibernate.Contracts;
using DigitalMenu.Infrastructure.Core.Data.Nhibernate.Conventions;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Diagnostics;
using System.Reflection;

namespace DigitalMenu.Infrastructure.Core.Data.Nhibernate
{
    public class FluentSessionFactoryBuilder : INhSessionFactoryBuilder
    {
        private readonly string schemaUpdateFileName = "schemaupdate.sql";
        private readonly NhibernateOptions options;
        protected readonly IConfiguration configuration;

        public FluentSessionFactoryBuilder(IOptions<NhibernateOptions> options, IConfiguration configuration)
        {
            this.options = options.Value;
            this.configuration = configuration;
        }

        public virtual ISessionFactory BuildSessionFactory()
        {
            var fluentConfig = Fluently.Configure(new NHibernate.Cfg.Configuration());

            ConfigureDatabase(fluentConfig);
            ConfigureMappings(fluentConfig);

            fluentConfig.ExposeConfiguration(c =>
            {
                SchemaUpdate(c);
            });

            return fluentConfig
                .BuildConfiguration()
                .BuildSessionFactory();
        }

        protected virtual void ConfigureDatabase(FluentConfiguration fluentConfig)
        {
            var connString = configuration.GetConnectionString(options.ConnectionStringName ?? "default");

            fluentConfig.Database(PostgreSQLConfiguration.PostgreSQL82
                        .DefaultSchema(options.DefaultSchema ?? "public")
                        .ConnectionString(connString));
        }

        protected virtual void ConfigureMappings(FluentConfiguration fluentConfig)
        {
            if (options.MappingAssemblies != null)
            {
                fluentConfig.Mappings(mappings =>
                {
                    mappings.FluentMappings.Conventions.Add(new CustomTableConvention());

                    foreach (var assemblyName in options.MappingAssemblies)
                    {
                        var assembly = Assembly.Load(assemblyName);

                        if (assembly == null)
                        {
                            throw new Exception($"'{assemblyName}' Assembly não encontrado.");
                        }

                        mappings.FluentMappings.AddFromAssembly(assembly);
                    }
                });
            }
        }

        protected virtual void SchemaUpdate(NHibernate.Cfg.Configuration config)
        {
            if (!(options.SchemaUpdate?.AllowsInvokeSchemaUpdate() ?? false))
            {
                return;
            }

            if (options.SchemaUpdate.SaveToFile)
            {
                var fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, schemaUpdateFileName);

                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }

                new SchemaUpdate(config).Execute((script) => GenerateSchemaUpdateScriptFile(fileName, script), options.SchemaUpdate.DoUpdate);
            }
            else
            {
                new SchemaUpdate(config).Execute(false, options.SchemaUpdate.DoUpdate);
            }
        }

        protected virtual void GenerateSchemaUpdateScriptFile(string fileName, string script)
        {
            using (var file = new FileStream(fileName, FileMode.Append, FileAccess.Write))
            {
                using (var writer = new StreamWriter(file))
                {
                    writer.WriteLine(script.Trim() + ";");
                    writer.Close();
                }

                if(Environment.de)
                Process externalProcess = new Process();
                externalProcess.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
                externalProcess.StartInfo.FileName = "Notepad.exe";
                externalProcess.StartInfo.Arguments = fileName;
                externalProcess.Start();
                externalProcess.WaitForExit();
            }
        }
    }
}
