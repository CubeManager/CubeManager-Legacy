import { ServerProperties } from './server-properties';
export class Server {
    constructor() {
        this.name = "";
        this.cpu = 0;
        this.memory = 0;
        this.state = "Started";
        this.currentPlayers = [];
        this.serverProperties = new ServerProperties();
    }
    public name: string;
    public cpu: number;
    public memory: number; 
    public state: string;
    public currentPlayers: any[];
    public serverProperties: ServerProperties;
}
