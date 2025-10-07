using System.Linq;
using Nuke.Common;
using Nuke.Common.Execution;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.IO;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.Tools.DotNet.DotNetTasks;
 
[UnsetVisualStudioEnvironmentVariables]
class Build : NukeBuild
{
    public static int Main() => Execute<Build>(x => x.Pack);

    [Parameter] 
    readonly AbsolutePath ArtifactsPath = RootDirectory + "/artifacts/";

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")] 
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Solution] 
    readonly Solution Solution;
     

    Target Clean => _ => _ 
        .Before(Restore)
        .Executes(() =>
        {
            // Clean bin and obj directories using modern Nuke APIs
            RootDirectory.GlobDirectories("Framework/src/**/bin", "Framework/src/**/obj").ForEach(dir => dir.DeleteDirectory());
            RootDirectory.GlobDirectories("Framework/test/**/bin", "Framework/test/**/obj").ForEach(dir => dir.DeleteDirectory());
            
            // Clean artifacts directory
            if (ArtifactsPath.DirectoryExists())
                ArtifactsPath.DeleteDirectory();
            
            // Ensure clean artifacts directory exists
            ArtifactsPath.CreateOrCleanDirectory();
        });

    Target Restore => _ => _
        .DependsOn(Clean)
        .Executes(() =>DotNetRestore(s => s.SetProjectFile(Solution)));

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            DotNetBuild(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .EnableNoRestore());
        });

    Target RunUnitTests => d => 
        d.DependsOn(Compile)
        .Executes(() =>
        {
            var testProjects = Solution.AllProjects
                .Where(p => p.Name.EndsWith("Tests") || p.Name.EndsWith("Tests.Unit"))
                .ToList();

            foreach (var testProject in testProjects)
            {
                DotNetTest(s => s
                    .SetProjectFile(testProject)
                    .SetConfiguration(Configuration)
                    .EnableNoBuild()
                    .EnableNoRestore());
            }
        });

    Target Pack => _ => _
        .DependsOn(RunUnitTests)
        .Executes(() =>
        {
            var projects = Solution.AllProjects
                .Where(p => !p.Name.EndsWith("Tests") && !p.Name.EndsWith("Tests.Unit"))
                .ToList();
                
            foreach (var project in projects)
            {
                DotNetPack(s => s
                    .SetProject(project)
                    .SetConfiguration(Configuration)
                    .EnableNoBuild()
                    .EnableNoRestore()
                    .SetVersion("1.0.0")
                    .SetOutputDirectory(ArtifactsPath));
            }
        });
}