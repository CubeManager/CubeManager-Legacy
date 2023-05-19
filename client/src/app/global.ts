import { Server } from './server.model';

export class GlobalVariables {
    public static servers: Server[] = [];
    public static ram: number = 0;
    public static storage: number = 0;
    public static cpu: number = 0;
}