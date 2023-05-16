using Domain.Enums;
using CrossCutting.Attributes;
namespace Service.InputModels;

public class ServerPropertiesInputModel
{
    [ServerPropertyName("spawn-protection")]
    public int? spawnProtection { get; set; }

    [ServerPropertyName("max-tick-time")]
    public int? maxTickTime { get; set; }

    [ServerPropertyName("force-gamemode")]
    public bool? forceGamemode { get; set; }

    [ServerPropertyName("allow-nether")]
    public bool? allowNether { get; set; }

    [ServerPropertyName("max-chained-neighbor-updates")]
    public int? maxChainedNeighborUpdates { get; set; }

    [ServerPropertyName("enable-query")]
    public bool? enableQuery { get; set; }

    [ServerPropertyName("query.port")]
    public int? queryPort { get; set; }

    [ServerPropertyName("player-idle-timeout")]
    public int? playerIdleTimeout { get; set; }

    [ServerPropertyName("spawn-monsters")]
    public bool? spawnMonsters { get; set; }

    [ServerPropertyName("pvp")]
    public bool? pvp { get; set; }

    [ServerPropertyName("hardcore")]
    public bool? hardcore { get; set; }

    [ServerPropertyName("enable-command-block")]
    public bool? enableCommandBlock { get; set; }

    [ServerPropertyName("max-players")]
    public int? maxPlayers { get; set; }

    [ServerPropertyName("server-port")]
    public int? serverPort { get; set; }

    [ServerPropertyName("max-players")]
    public int? maxPlayers { get; set; }

    [ServerPropertyName("server-ip")]
    public string? serverIp { get; set; }

    [ServerPropertyName("spawn-npcs")]
    public bool? spawnNpcs { get; set; }

    [ServerPropertyName("allow-flight")]
    public bool? allowFlight { get; set; }

    [ServerPropertyName("level-name")]
    public string? levelName { get; set; }

    [ServerPropertyName("view-distance")]
    public int? viewDistance { get; set; }

    [ServerPropertyName("spawn-animals")]
    public bool? spawnAnimals { get; set; }

    [ServerPropertyName("white-list")]
    public bool? whiteList { get; set; }

    [ServerPropertyName("generate-structures")]
    public bool? generateStructures { get; set; }

    [ServerPropertyName("online-mode")]
    public bool? onlineMode { get; set; }

    [ServerPropertyName("level-seed")]
    public string? levelSeed { get; set; }

    [ServerPropertyName("use-native-transport")]
    public bool? useNativeTransport { get; set; }

    [ServerPropertyName("motd")]
    public string? motd { get; set; }

    [ServerPropertyName("enable-rcon")]
    public bool? enableRcon { get; set; }

    [ServerPropertyName("rcon.password")]
    public string? rconPassword { get; set; }

    [ServerPropertyName("rcon.port")]
    public int? rconPort { get; set; }

    [ServerPropertyName("max-world-size")]
    public int? maxWorldSize { get; set; }

    [ServerPropertyName("resource-pack-sha1")]
    public string? resourcePackSha1 { get; set; }

    [ServerPropertyName("enforce-whitelist")]
    public bool? enforceWhitelist { get; set; }

    [ServerPropertyName("initial-enabled-packs")]
    public string? initialEnabledPacks { get; set; }

    [ServerPropertyName("simulation-distance")]
    public int? simulationDistance { get; set; }

    [ServerPropertyName("rate-limit")]
    public int? rateLimit { get; set; }

    [ServerPropertyName("hide-online-players")]
    public bool? hideOnlinePlayers { get; set; }

    [ServerPropertyName("prevent-proxy-connections")]
    public bool? preventProxyConnections { get; set; }

    [ServerPropertyName("sync-chunk-writes")]
    public bool? syncChunkWrites { get; set; }

    [ServerPropertyName("broadcast-rcon-to-ops")]
    public bool? broadcastRconToOps { get; set; }

    [ServerPropertyName("enable-status")]
    public bool? enableStatus { get; set; }

    [ServerPropertyName("network-compression-threshold")]
    public int? networkCompressionThreshold { get; set; }

    [ServerPropertyName("enforce-secure-profile")]
    public bool? enforceSecureProfile { get; set; }

    [ServerPropertyName("enable-jmx-monitoring")]
    public bool? enableJmxMonitoring { get; set; }

    [ServerPropertyName("require-resource-pack")]
    public bool? requireResourcePack { get; set; }

    [ServerPropertyName("entity-broadcast-range-percentage")]
    public int? entityBroadcastRangePercentage { get; set; }

    [ServerPropertyName("broadcast-console-to-ops")]
    public bool? broadcastConsoleToOps { get; set; }

    [ServerPropertyName("resource-pack")]
    public string? resourcePack { get; set; }

    [ServerPropertyName("resource-pack-prompt")]
    public string? resourcePackPrompt { get; set; }

    [ServerPropertyName("initial-disabled-packs")]
    public string? initialDisabledPacks { get; set; }

    [ServerPropertyName("generator-settings")]
    public string? generatorSettings { get; set; }

    [ServerPropertyName("difficulty")]
    public Difficulty? difficulty { get; set; }

    [ServerPropertyName("gamemode")]
    public Gamemode? gamemode { get; set; }

    [ServerPropertyName("level-type")]
    public LevelType? levelType { get; set; }

    [ServerPropertyName("op-permission-level")]
    public OpPermissionLevel? opPermissionLevel { get; set; }
}
