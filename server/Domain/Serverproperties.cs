namespace Domain;

using Domain.Enums;
using System.Text.Json.Serialization;

//documentation: https://minecraft.fandom.com/wiki/Server.properties
public class ServerProperties
{
    [JsonPropertyName("spawn_protection")]
    public int spawnProtection { get; set; }

    [JsonPropertyName("max-tick-tim")]
    public int maxTickTime { get; set; }

    [JsonPropertyName("force-gamemode")]
    public bool forceGamemode { get; set; }

    [JsonPropertyName("allow-nether")]
    public bool allowNether { get; set; }

    [JsonPropertyName("max-chained-neighbor-updates")]
    public int maxChainedNeighborUpdates { get; set; }

    [JsonPropertyName("enable-query")]
    public bool enableQuery { get; set; }

    [JsonPropertyName("query.port")]
    public int queryPort { get; set; }

    [JsonPropertyName("player-idle-timeout")]
    public int playerIdleTimeout { get; set; }

    [JsonPropertyName("spawn-monsters")]
    public bool spawnMonsters { get; set; }

    [JsonPropertyName("pvp")]
    public bool pvp { get; set; }

    [JsonPropertyName("snooper-enabled")]
    public bool snooperEnabled { get; set; }

    [JsonPropertyName("hardcore")]
    public bool hardcore { get; set; }

    [JsonPropertyName("enable-command-block")]
    public bool enableCommandBlock { get; set; }

    [JsonPropertyName("max-players")]
    public int maxPlayers { get; set; }

    [JsonPropertyName("server-port")]
    public int serverPort { get; set; }

    [JsonPropertyName("server-ip")]
    public string? serverIp { get; set; }

    [JsonPropertyName("spawn-npcs")]
    public bool spawnNpcs { get; set; }

    [JsonPropertyName("allow-flight")]
    public bool allowFlight { get; set; }

    [JsonPropertyName("level-name")]
    public string? levelName { get; set; }

    [JsonPropertyName("view-distance")]
    public int viewDistance { get; set; }

    [JsonPropertyName("spawn-animals")]
    public bool spawnAnimals { get; set; }

    [JsonPropertyName("white-list")]
    public bool whiteList { get; set; }

    [JsonPropertyName("generate-structures")]
    public bool generateStructures { get; set; }

    [JsonPropertyName("online-mode")]
    public bool onlineMode { get; set; }

    [JsonPropertyName("max-building-height")]
    public int maxBuildingHeight { get; set; }

    [JsonPropertyName("level-seed")]
    public string? levelSeed { get; set; }

    [JsonPropertyName("use-native-transport")]
    public bool useNativeTransport { get; set; }

    [JsonPropertyName("modt")]
    public string? motd { get; set; }

    [JsonPropertyName("enable-rcon")]
    public bool enableRcon { get; set; }

    [JsonPropertyName("rcon.password")]
    public string? rconPassword { get; set; }

    [JsonPropertyName("rcon.port")]
    public int rconPort { get; set; }

    [JsonPropertyName("rcon.password")]
    public int maxWorldSize { get; set; }

    [JsonPropertyName("resource-pack-sha1")]
    public string? resourcePackSha1 { get; set; }

    [JsonPropertyName("enforce-white-list")]
    public bool enforceWhitelist { get; set; }

    [JsonPropertyName("initial-enabled-packs")]
    public string? initialEnabledPacks { get; set; }

    [JsonPropertyName("simulation-distance")]
    public int simulationDistance { get; set; }

    [JsonPropertyName("rate-limit")]
    public int rateLimit { get; set; }

    [JsonPropertyName("hide-online-players")]
    public bool hideOnlinePlayers { get; set; }

    [JsonPropertyName("prevent-proxy-connections")]
    public bool preventProxyConnections { get; set; }

    [JsonPropertyName("sync-chunk-writes")]
    public bool syncChunkWrites { get; set; }

    [JsonPropertyName("broadcast-rcon-to-ops")]
    public bool broadcastRconToOps { get; set; }

    [JsonPropertyName("enable-status")]
    public bool enableStatus { get; set; }

    [JsonPropertyName("network-compression-threshold")]
    public int networkCompressionThreshold { get; set; }

    [JsonPropertyName("enforce-secure-profile")]
    public bool enforceSecureProfile { get; set; }

    [JsonPropertyName("enable-jmx-monitoring")]
    public bool enableJmxMonitoring { get; set; }

    [JsonPropertyName("require-resource-pack")]
    public bool requireResourcePack { get; set; }

    [JsonPropertyName("entity-broadcast-range-percentage")]
    public int entityBroadcastRangePercentage { get; set; }

    [JsonPropertyName("broadcast-console-to-ops")]
    public bool broadcastConsoleToOps { get; set; }

    [JsonPropertyName("resource-pack")]
    public string? resourcePack { get; set; }

    [JsonPropertyName("resource-pack-prompt")]
    public string? resourcePackPrompt { get; set; }

    [JsonPropertyName("initial-disabled-packs")]
    public string? initialDisabledPacks { get; set; }

    [JsonPropertyName("generator-settings")]
    public string? generatorSettings { get; set; }

    [JsonPropertyName("difficulty")]
    public Difficulty difficulty { get; set; }

    [JsonPropertyName("gamemode")]
    public Gamemode gamemode { get; set; }

    [JsonPropertyName("level-type")]
    public LevelType levelType { get; set; }

    [JsonPropertyName("op-permission-level")]
    public OpPermissionLevel opPermissionLevel { get; set; }
}
