using System.Collections.Generic;
using CrackTower.WebServiceTemplate.NukeBuild;
using CrackTower.WebServiceTemplate.NukeBuild.Common;
using CrackTower.WebServiceTemplate.NukeBuild.Docker;
using CrackTower.WebServiceTemplate.NukeBuild.Extensions;
using Nuke.Common;

class Build : NukeBuild, IDefaultBuildFlow
{
    public string ServiceName => "TickerQ.Jobs";

    public ApplicationVersion Version => this.UseSemanticVersion(major: 1, minor: 0);

    public bool ExecuteIntegrationTests => true;

    public IReadOnlyList<IDockerImageInfo> DockerImages { get; } = new[]
    {
        new GeneratedDockerImageInfo(DockerImageName: "tickerq-jobs-dashboard", ProjectName: "Dashboard"),
        new GeneratedDockerImageInfo(DockerImageName: "tickerq-jobs-node", ProjectName: "Node"),
    };

    public static int Main()
        => Execute<Build>(x => ((IDefaultBuildFlow)x).Default);
}
