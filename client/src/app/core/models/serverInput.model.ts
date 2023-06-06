import { ServerPropertiesInput } from "./serverPropertiesInput.model";

export class ServerInput {
  constructor(
    public serverName: string | null | undefined,
    public serverType: string | null | undefined,
    public exactVersion: string | null | undefined,
    public maxMemory: number | null | undefined,
    public serverFileName: string | null | undefined,
    public serverProperties: ServerPropertiesInput | null

  ) {}
}
