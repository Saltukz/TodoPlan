import { Developer } from "../../project/project.model";

export interface ResponseProjectGet {
  Name: string | null;
  Time: number | null;
  lstDeveloper: Developer[];
}
