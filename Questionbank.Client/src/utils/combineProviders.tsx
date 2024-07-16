export const combineProviders = (list : any, children : any) =>
  list.reduceRight((acc : any, Provider : any) => <Provider>{acc}</Provider>, children);