import { Keyword } from './Keyword';

export interface Record {
  id: string;
  stareaGenerala: string;
  talie: number;
  greutate: number;
  nutritie: string;
  constienta: string;
  facies: string;
  tegumente: string;
  mucoase: string;
  fanere: string;
  tesutConjunctiv: string;
  sistemGanglionar: string;
  sistemMuscular: string;
  sistemOsteoArticular: string;
  aparatRespirator: string;
  aparatCardiovascular: string;
  aparatDigestiv: string;
  ficatSplina: string;
  aparatUroGeneral: string;
  sistemEndocrin: string;
  motiveleInternarii: string;
  anamneza: string;
  istoriculBolii: string;
  diagnosis: string;
  keywords: Keyword[];
}
