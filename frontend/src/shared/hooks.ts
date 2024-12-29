import { TypedUseSelectorHook, useDispatch, useSelector } from 'react-redux';
import { RefObject, useEffect, useState } from 'react';
import type { RootState, AppDispatch } from '../app/store.ts';

// юзати їх замість useSelector, useDispatch, тоді не буде помилок по типізації
export const useAppDispatch = () => useDispatch<AppDispatch>();
export const useAppSelector: TypedUseSelectorHook<RootState> = useSelector;

// переобчислює ширину елемента при зміні ширини вікна
export const useWidthRecalculate = (
  /* syntax: const [w, setW] = useWidthRecalculate(itemRef) */
  item: RefObject<HTMLElement>,
) => {
  const { screenWidth } = useAppSelector(st => st.global);
  const [itemWidth, setItemWidth] = useState(0);

  useEffect(() => {
    if (item.current) {
      setItemWidth(item.current.getBoundingClientRect().width);
    }
  }, [screenWidth]);

  // щоб підказати ТС, що повертається яке значення на якому місці
  return [itemWidth, setItemWidth] as [
    number,
    React.Dispatch<React.SetStateAction<number>>,
  ];
};
