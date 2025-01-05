import { Action, configureStore, ThunkAction } from '@reduxjs/toolkit';
import globalReducer from '../features/globalSlice.ts';

const store = configureStore({
  reducer: {
    // для глобальних даних(ширина екрану, чи відкр меню)
    global: globalReducer,
  },
});

export default store;
// ця вся діч для проходження типізації
export type AppDispatch = typeof store.dispatch;
export type RootState = ReturnType<typeof store.getState>;
/* eslint-disable @typescript-eslint/indent */
export type AppThunk<ReturnType = void> = ThunkAction<
  ReturnType,
  RootState,
  unknown,
  Action<string>
>;
/* eslint-enable @typescript-eslint/indent */
