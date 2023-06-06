import { ServerType } from "../enums/server-type.enum";
import { ServerProperties } from "./server-properties.model";

export interface Server {
  serverName: string | null;
  isRunning: boolean;
  cpu: number;
  memory: number;
  currentPlayers: number;
  serverType: ServerType;
  exactVersion: string | null;
  maxMemory: number;
  serverProperties: ServerProperties;
}
