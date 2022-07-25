#pragma once

#define QDELETE(x)  if(x) { delete x; x = 0; }
#define QRELEASE(x) if(x) { x->Release(); x = 0; }

//void BuildGraph(PGRAPH_CONTROL pCtrl, GS_SETTINGS *pSettings);
void BuildGraphRefact(PGRAPH_CONTROL pCtrl, GS_SETTINGSRefact * pSettings);
void SetRTPSource(PGRAPH_CONTROL pCtrl, INPUT_NETWORK *pInNet);
