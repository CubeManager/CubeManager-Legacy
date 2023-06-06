import { ServerType } from "../enums/server-type.enum";
import { ServerProperties } from "./server-properties.model";

export interface Server {
  serverName: string | null | undefined;
  isRunning?: boolean;
  cpu?: number;
  memory?: number;
  currentPlayers?: number;
  serverType: ServerType | null | undefined;
  exactVersion?: string | null;
  maxMemory?: number;
  serverProperties: ServerProperties;
  serverFileName?: string;
}
