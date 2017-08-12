import { CompetitorData } from "./competitor-data";
import { CompetitorItem } from "./competitor-item";
import { CompetitorField } from "./competitor-field";

export class Competitor {
    public Id: number;
    public Name: string;
    public Url: string;
    public ProjectId:number;
    public Email: string; 
    public Status: number; 
    public TestUrl: string; 

    public CompetitorData: CompetitorData[];
    public CompetitorField: CompetitorField[];
    public CompetitorItem: CompetitorItem[];
}
