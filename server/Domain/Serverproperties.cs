namespace domain;

//documentation: https://minecraft.fandom.com/wiki/Server.properties
public class ServerProperties
{
    public int spawnProtection {get; set;}

    public int maxTickTime {get; set;}
    
    public bool forceGamemode {get; set;}

    public bool allowNether {get; set;}

    public int maxChainedNeighborUpdates {get; set;}

    public bool enableQuery {get; set;}

    public int queryPort {get; set;}

    public int playerIdleTimeout {get; set;}

    public bool spawnMonsters {get; set;}

    public bool pvp {get; set;}

    public bool snooperEnabled {get; set;}

    public bool hardcore {get; set;}

    public bool enableCommandBlock {get; set;}

    public int maxPlayers {get; set;}

    public int serverPort {get; set;}

    public string serverIp {get; set;}

    public bool spawnNpcs {get; set;}

    public bool allowFlight {get; set;}

    public string levelName {get; set;}

    public int viewDistance {get; set;}

    public bool spawnAnimals {get; set;}

    public bool whiteList {get; set;}

    public bool generateStructures {get; set;}

    public bool onlineMode {get; set;}

    public int maxBuildingHeight {get; set;}

    public string levelSeed {get; set;}

    public bool useNativeTransport {get; set;}

    public string motd {get; set;}

    public bool enableRcon {get; set;}

    public string rconPassword {get; set;}

    public int rconPort {get; set;}

    public int maxWorldSize {get; set;}

    public string resourcePackSha1 {get; set;}

    public bool enforceWhitelist {get; set;}

    public string initialEnabledPacks {get; set;}

    public int simulationDistance {get; set;}

    public int rateLimit {get: set;}

    public bool hideOnlinePlayers {get; set;}

    public bool preventProxyConnections {get; set;}

    public bool syncChunkWrites {get; set;}

    public bool broadcastRconToOps {get; set;}

    public bool enableStatus {get; set;}

    public bool useNativeTransport {get; set;}

    public int networkCompressionThreshold {get; set;}

    public bool enforceSecureProfile {get; set;}

    public bool enableJmxMonitoring {get;set;}

    public bool requireResourcePack {get; set;}

    public int entityBroadcastRangePercentage {get; set;}

    public bool broadcastConsoleToOps {get; set;}

    public string resourcePack {get; set;}

    public string resourcePackPrompt {get; set;}

    public string initialDisabledPacks {get; set;}

    public string generatorSettings {get; set;}

    public Difficulty difficulty {get; set;}

    public Gamemode gamemode {get; set;}

    public LevelType levelType {get; set;}

    public OpPermissionLevel opPermissionLevel {get; set;}
}
