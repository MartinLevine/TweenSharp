using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TweenSharp
{
    /// <summary>
    /// 动画类别
    /// </summary>
    public enum AnimationType
    {
        EASE_IN, EASE_OUT, EASE_IN_OUT
    }

    public delegate void AnimateEvent(int posi);

    /// <summary>
    /// 无缓动特效
    /// </summary>
    public class Linear
    {
        /// <summary>
        /// 异步调用缓动方法
        /// </summary>
        /// <param name="context">需要更新UI的线程上下文</param>
        /// <param name="currentTime">初始缓动步数</param>
        /// <param name="beginPosition">初始位置</param>
        /// <param name="variablePosition">位置变化量</param>
        /// <param name="duration">目标缓动步数</param>
        /// <param name="sleepMillSecond">延迟时间，以毫秒为单位</param>
        /// <param name="animateEvent">缓动动画回调</param>
        public static void setOnLinerAnimateCallback(SynchronizationContext context, double currentTime, int beginPosition, int variablePosition, double duration, int sleepMillSecond, AnimateEvent animateEvent)
        {
            new Thread(() =>
            {
                int posi = beginPosition;
                while (currentTime < duration)
                {
                    posi = (int)easeIn(currentTime++, beginPosition, variablePosition, duration);
                    context.Post((n) => {
                        animateEvent((int)n);
                    }, posi);
                    Thread.Sleep(sleepMillSecond);
                }
            }).Start();
        }

        /// <summary>
        /// 同步调用缓动方法，缓动前半段
        /// </summary>
        /// <param name="currentTime">初始缓动参数</param>
        /// <param name="beginPosition">起始位置</param>
        /// <param name="variablePosition">位置变化量</param>
        /// <param name="duration">目标缓动参数</param>
        /// <returns>返回单次缓动后的位置</returns>
        public static double easeIn(double currentTime, int beginPosition, int variablePosition, double duration)
        {
            //Console.WriteLine((double)variablePosition * currentTime / duration + beginPosition);
            return (double)variablePosition * currentTime / duration + beginPosition;
        }

        /// <summary>
        /// 同步调用缓动方法，缓动后半段
        /// </summary>
        /// <param name="currentTime">初始缓动参数</param>
        /// <param name="beginPosition">起始位置</param>
        /// <param name="variablePosition">位置变化量</param>
        /// <param name="duration">目标缓动参数</param>
        /// <returns>返回单次缓动后的位置</returns>
        public static double easeOut(double currentTime, int beginPosition, int variablePosition, double duration)
        {
            return (double)variablePosition * currentTime / duration + beginPosition;
        }

        /// <summary>
        /// 同步调用缓动方法，缓动整个时间段
        /// </summary>
        /// <param name="currentTime">初始缓动参数</param>
        /// <param name="beginPosition">起始位置</param>
        /// <param name="variablePosition">位置变化量</param>
        /// <param name="duration">目标缓动参数</param>
        /// <returns>返回单次缓动后的位置</returns>
        public static double easeInOut(double currentTime, int beginPosition, int variablePosition, double duration)
        {
            return (double)variablePosition * currentTime / duration + beginPosition;
        }
    }

    /// <summary>
    /// 二次方缓动
    /// </summary>
    public class Quad
    {
        /// <summary>
        /// 异步调用缓动方法
        /// </summary>
        /// <param name="context">需要更新UI的线程上下文</param>
        /// <param name="currentTime">初始缓动步数</param>
        /// <param name="beginPosition">初始位置</param>
        /// <param name="variablePosition">位置变化量</param>
        /// <param name="duration">目标缓动步数</param>
        /// <param name="sleepMillSecond">延迟时间，以毫秒为单位</param>
        /// <param name="animateEvent">缓动动画回调</param>
        public static void setOnQuadAnimateCallback(SynchronizationContext context,double currentTime, int beginPosition, int variablePosition, double duration, int sleepMillSecond,AnimateEvent animateEvent, AnimationType type)
        {
            new Thread(() =>
            {
                int posi = beginPosition;
                while (currentTime < duration)
                {
                    switch (type)
                    {
                        case AnimationType.EASE_IN:
                            posi = (int)easeIn(currentTime++, beginPosition, variablePosition, duration);
                            break;
                        case AnimationType.EASE_OUT:
                            posi = (int)easeOut(currentTime++, beginPosition, variablePosition, duration);
                            break;
                        case AnimationType.EASE_IN_OUT:
                            posi = (int)easeInOut(currentTime++, beginPosition, variablePosition, duration);
                            break;
                    }
                    context.Post((n) =>{
                        animateEvent((int)n);
                    }, posi);
                    Thread.Sleep(sleepMillSecond);
                }
            }).Start();
        }

        /// <summary>
        /// 同步调用缓动方法，缓动前半段
        /// </summary>
        /// <param name="currentTime">初始缓动参数</param>
        /// <param name="beginPosition">起始位置</param>
        /// <param name="variablePosition">位置变化量</param>
        /// <param name="duration">目标缓动参数</param>
        /// <returns>返回单次缓动后的位置</returns>
        public static double easeIn(double currentTime, int beginPosition, int variablePosition, double duration)
        {
            return variablePosition * (currentTime /= duration) * currentTime + beginPosition;
        }

        /// <summary>
        /// 同步调用缓动方法，缓动后半段
        /// </summary>
        /// <param name="currentTime">初始缓动参数</param>
        /// <param name="beginPosition">起始位置</param>
        /// <param name="variablePosition">位置变化量</param>
        /// <param name="duration">目标缓动参数</param>
        /// <returns>返回单次缓动后的位置</returns>
        public static double easeOut(double currentTime, int beginPosition, int variablePosition, double duration)
        {
            return -variablePosition * (currentTime /= duration) * (currentTime - 2) + beginPosition;
        }

        /// <summary>
        /// 同步调用缓动方法，缓动整个时间段
        /// </summary>
        /// <param name="currentTime">初始缓动参数</param>
        /// <param name="beginPosition">起始位置</param>
        /// <param name="variablePosition">位置变化量</param>
        /// <param name="duration">目标缓动参数</param>
        /// <returns>返回单次缓动后的位置</returns>
        public static double easeInOut(double currentTime, int beginPosition, int variablePosition, double duration)
        {
            if ((currentTime /= duration / 2) < 1) return variablePosition / 2 * currentTime * currentTime + beginPosition;
            return -variablePosition / 2 * ((--currentTime) * (currentTime - 2) - 1) + beginPosition;
        }
    }

    /// <summary>
    /// 三次方缓动
    /// </summary>
    public class Cubic
    {
        /// <summary>
        /// 异步调用缓动方法
        /// </summary>
        /// <param name="context">需要更新UI的线程上下文</param>
        /// <param name="currentTime">初始缓动步数</param>
        /// <param name="beginPosition">初始位置</param>
        /// <param name="variablePosition">位置变化量</param>
        /// <param name="duration">目标缓动步数</param>
        /// <param name="sleepMillSecond">延迟时间，以毫秒为单位</param>
        /// <param name="animateEvent">缓动动画回调</param>
        public static void setOnCubicAnimateCallback(SynchronizationContext context, double currentTime, int beginPosition, int variablePosition, double duration, int sleepMillSecond, AnimateEvent animateEvent, AnimationType type)
        {
            new Thread(() =>
            {
                int posi = beginPosition;
                while (currentTime < duration)
                {
                    switch (type)
                    {
                        case AnimationType.EASE_IN:
                            posi = (int)easeIn(currentTime++, beginPosition, variablePosition, duration);
                            break;
                        case AnimationType.EASE_OUT:
                            posi = (int)easeOut(currentTime++, beginPosition, variablePosition, duration);
                            break;
                        case AnimationType.EASE_IN_OUT:
                            posi = (int)easeInOut(currentTime++, beginPosition, variablePosition, duration);
                            break;
                    }
                    context.Post((n) => {
                        animateEvent((int)n);
                    }, posi);
                    Thread.Sleep(sleepMillSecond);
                }
            }).Start();
        }

        /// <summary>
        /// 同步调用缓动方法，缓动前半段
        /// </summary>
        /// <param name="currentTime">初始缓动参数</param>
        /// <param name="beginPosition">起始位置</param>
        /// <param name="variablePosition">位置变化量</param>
        /// <param name="duration">目标缓动参数</param>
        /// <returns>返回单次缓动后的位置</returns>
        public static double easeIn(double currentTime, int beginPosition, int variablePosition, double duration)
        {
            return variablePosition * (currentTime /= duration) * currentTime * currentTime + beginPosition;
        }

        /// <summary>
        /// 同步调用缓动方法，缓动后半段
        /// </summary>
        /// <param name="currentTime">初始缓动参数</param>
        /// <param name="beginPosition">起始位置</param>
        /// <param name="variablePosition">位置变化量</param>
        /// <param name="duration">目标缓动参数</param>
        /// <returns>返回单次缓动后的位置</returns>
        public static double easeOut(double currentTime, int beginPosition, int variablePosition, double duration)
        {
            return variablePosition * ((currentTime = currentTime / duration - 1) * currentTime * currentTime + 1) + beginPosition;
        }

        /// <summary>
        /// 同步调用缓动方法，缓动整个时间段
        /// </summary>
        /// <param name="currentTime">初始缓动参数</param>
        /// <param name="beginPosition">起始位置</param>
        /// <param name="variablePosition">位置变化量</param>
        /// <param name="duration">目标缓动参数</param>
        /// <returns>返回单次缓动后的位置</returns>
        public static double easeInOut(double currentTime, int beginPosition, int variablePosition, double duration)
        {
            if ((currentTime /= duration / 2) < 1) return variablePosition / 2 * currentTime * currentTime * currentTime + beginPosition;
            return variablePosition / 2 * ((currentTime -= 2) * currentTime * currentTime + 2) + beginPosition;
        }
    }

    /// <summary>
    /// 四次方缓动
    /// </summary>
    public class Quart
    {
        /// <summary>
        /// 异步调用缓动方法
        /// </summary>
        /// <param name="context">需要更新UI的线程上下文</param>
        /// <param name="currentTime">初始缓动步数</param>
        /// <param name="beginPosition">初始位置</param>
        /// <param name="variablePosition">位置变化量</param>
        /// <param name="duration">目标缓动步数</param>
        /// <param name="sleepMillSecond">延迟时间，以毫秒为单位</param>
        /// <param name="animateEvent">缓动动画回调</param>
        public static void setOnQuartAnimationCallBack(SynchronizationContext context, double currentTime, int beginPosition, int variablePosition, double duration, int sleepMillSecond, AnimateEvent animateEvent, AnimationType type)
        {
            new Thread(() =>
            {
                int posi = beginPosition;
                while (currentTime < duration)
                {
                    switch (type)
                    {
                        case AnimationType.EASE_IN:
                            posi = (int)easeIn(currentTime++, beginPosition, variablePosition, duration);
                            break;
                        case AnimationType.EASE_OUT:
                            posi = (int)easeOut(currentTime++, beginPosition, variablePosition, duration);
                            break;
                        case AnimationType.EASE_IN_OUT:
                            posi = (int)easeInOut(currentTime++, beginPosition, variablePosition, duration);
                            break;
                    }
                    context.Post((n) => {
                        animateEvent((int)n);
                    }, posi);
                    Thread.Sleep(sleepMillSecond);
                }
            }).Start();
        }

        /// <summary>
        /// 同步调用缓动方法，缓动前半段
        /// </summary>
        /// <param name="currentTime">初始缓动参数</param>
        /// <param name="beginPosition">起始位置</param>
        /// <param name="variablePosition">位置变化量</param>
        /// <param name="duration">目标缓动参数</param>
        /// <returns>返回单次缓动后的位置</returns>
        public static double easeIn(double currentTime, int beginPosition, int variablePosition, double duration)
        {
            return variablePosition * (currentTime /= duration) * currentTime * currentTime * currentTime + beginPosition;
        }

        /// <summary>
        /// 同步调用缓动方法，缓动后半段
        /// </summary>
        /// <param name="currentTime">初始缓动参数</param>
        /// <param name="beginPosition">起始位置</param>
        /// <param name="variablePosition">位置变化量</param>
        /// <param name="duration">目标缓动参数</param>
        /// <returns>返回单次缓动后的位置</returns>
        public static double easeOut(double currentTime, int beginPosition, int variablePosition, double duration)
        {
            return -variablePosition * ((currentTime = currentTime / duration - 1) * currentTime * currentTime * currentTime - 1) + beginPosition;
        }

        /// <summary>
        /// 同步调用缓动方法，缓动整个时间段
        /// </summary>
        /// <param name="currentTime">初始缓动参数</param>
        /// <param name="beginPosition">起始位置</param>
        /// <param name="variablePosition">位置变化量</param>
        /// <param name="duration">目标缓动参数</param>
        /// <returns>返回单次缓动后的位置</returns>
        public static double easeInOut(double currentTime, int beginPosition, int variablePosition, double duration)
        {
            if ((currentTime /= duration / 2) < 1) return variablePosition / 2 * currentTime * currentTime * currentTime * currentTime + beginPosition;
            return -variablePosition / 2 * ((currentTime -= 2) * currentTime * currentTime * currentTime - 2) + beginPosition;
        }
    }

    /// <summary>
    /// 五次方缓动
    /// </summary>
    public class Quint
    {
        /// <summary>
        /// 异步调用缓动方法
        /// </summary>
        /// <param name="context">需要更新UI的线程上下文</param>
        /// <param name="currentTime">初始缓动步数</param>
        /// <param name="beginPosition">初始位置</param>
        /// <param name="variablePosition">位置变化量</param>
        /// <param name="duration">目标缓动步数</param>
        /// <param name="sleepMillSecond">延迟时间，以毫秒为单位</param>
        /// <param name="animateEvent">缓动动画回调</param>
        public static void setOnQuintAnimateCallback(SynchronizationContext context, double currentTime, int beginPosition, int variablePosition, double duration, int sleepMillSecond, AnimateEvent animateEvent, AnimationType type)
        {
            new Thread(() =>
            {
                int posi = beginPosition;
                while (currentTime < duration)
                {
                    switch (type)
                    {
                        case AnimationType.EASE_IN:
                            posi = (int)easeIn(currentTime++, beginPosition, variablePosition, duration);
                            break;
                        case AnimationType.EASE_OUT:
                            posi = (int)easeOut(currentTime++, beginPosition, variablePosition, duration);
                            break;
                        case AnimationType.EASE_IN_OUT:
                            posi = (int)easeInOut(currentTime++, beginPosition, variablePosition, duration);
                            break;
                    }
                    context.Post((n) => {
                        animateEvent((int)n);
                    }, posi);
                    Thread.Sleep(sleepMillSecond);
                }
            }).Start();
        }

        /// <summary>
        /// 同步调用缓动方法，缓动前半段
        /// </summary>
        /// <param name="currentTime">初始缓动参数</param>
        /// <param name="beginPosition">起始位置</param>
        /// <param name="variablePosition">位置变化量</param>
        /// <param name="duration">目标缓动参数</param>
        /// <returns>返回单次缓动后的位置</returns>
        public static double easeIn(double currentTime, int beginPosition, int variablePosition, double duration)
        {
            return variablePosition * (currentTime /= duration) * currentTime * currentTime * currentTime * currentTime + beginPosition;
        }

        /// <summary>
        /// 同步调用缓动方法，缓动后半段
        /// </summary>
        /// <param name="currentTime">初始缓动参数</param>
        /// <param name="beginPosition">起始位置</param>
        /// <param name="variablePosition">位置变化量</param>
        /// <param name="duration">目标缓动参数</param>
        /// <returns>返回单次缓动后的位置</returns>
        public static double easeOut(double currentTime, int beginPosition, int variablePosition, double duration)
        {
            return variablePosition * ((currentTime = currentTime / duration - 1) * currentTime * currentTime * currentTime * currentTime + 1) + beginPosition;
        }

        /// <summary>
        /// 同步调用缓动方法，缓动整个时间段
        /// </summary>
        /// <param name="currentTime">初始缓动参数</param>
        /// <param name="beginPosition">起始位置</param>
        /// <param name="variablePosition">位置变化量</param>
        /// <param name="duration">目标缓动参数</param>
        /// <returns>返回单次缓动后的位置</returns>
        public static double easeInOut(double currentTime, int beginPosition, int variablePosition, double duration)
        {
            if ((currentTime /= duration / 2) < 1) return variablePosition / 2 * currentTime * currentTime * currentTime * currentTime * currentTime + beginPosition;
            return variablePosition / 2 * ((currentTime -= 2) * currentTime * currentTime * currentTime * currentTime + 2) + beginPosition;
        }
    }

    /// <summary>
    /// 正弦曲线缓动
    /// </summary>
    public class Sine
    {
        /// <summary>
        /// 异步调用缓动方法
        /// </summary>
        /// <param name="context">需要更新UI的线程上下文</param>
        /// <param name="currentTime">初始缓动步数</param>
        /// <param name="beginPosition">初始位置</param>
        /// <param name="variablePosition">位置变化量</param>
        /// <param name="duration">目标缓动步数</param>
        /// <param name="sleepMillSecond">延迟时间，以毫秒为单位</param>
        /// <param name="animateEvent">缓动动画回调</param>
        public static void setOnSineAnimateCallback(SynchronizationContext context, double currentTime, int beginPosition, int variablePosition, double duration, int sleepMillSecond, AnimateEvent animateEvent, AnimationType type)
        {
            new Thread(() =>
            {
                int posi = beginPosition;
                while (currentTime < duration)
                {
                    switch (type)
                    {
                        case AnimationType.EASE_IN:
                            posi = (int)easeIn(currentTime++, beginPosition, variablePosition, duration);
                            break;
                        case AnimationType.EASE_OUT:
                            posi = (int)easeOut(currentTime++, beginPosition, variablePosition, duration);
                            break;
                        case AnimationType.EASE_IN_OUT:
                            posi = (int)easeInOut(currentTime++, beginPosition, variablePosition, duration);
                            break;
                    }
                    context.Post((n) => {
                        animateEvent((int)n);
                    }, posi);
                    Thread.Sleep(sleepMillSecond);
                }
            }).Start();
        }

        /// <summary>
        /// 同步调用缓动方法，缓动前半段
        /// </summary>
        /// <param name="currentTime">初始缓动参数</param>
        /// <param name="beginPosition">起始位置</param>
        /// <param name="variablePosition">位置变化量</param>
        /// <param name="duration">目标缓动参数</param>
        /// <returns>返回单次缓动后的位置</returns>
        public static double easeIn(double currentTime, int beginPosition, int variablePosition, double duration)
        {
            return -variablePosition * (double)Math.Cos(currentTime / duration * (Math.PI / 2)) + variablePosition + beginPosition;
        }

        /// <summary>
        /// 同步调用缓动方法，缓动后半段
        /// </summary>
        /// <param name="currentTime">初始缓动参数</param>
        /// <param name="beginPosition">起始位置</param>
        /// <param name="variablePosition">位置变化量</param>
        /// <param name="duration">目标缓动参数</param>
        /// <returns>返回单次缓动后的位置</returns>
        public static double easeOut(double currentTime, int beginPosition, int variablePosition, double duration)
        {
            return variablePosition * (double)Math.Sin(currentTime / duration * (Math.PI / 2)) + beginPosition;
        }

        /// <summary>
        /// 同步调用缓动方法，缓动整个时间段
        /// </summary>
        /// <param name="currentTime">初始缓动参数</param>
        /// <param name="beginPosition">起始位置</param>
        /// <param name="variablePosition">位置变化量</param>
        /// <param name="duration">目标缓动参数</param>
        /// <returns>返回单次缓动后的位置</returns>
        public static double easeInOut(double currentTime, int beginPosition, int variablePosition, double duration)
        {
            return -variablePosition / 2 * ((double)Math.Cos(Math.PI * currentTime / duration) - 1) + beginPosition;
        }
    }

    /// <summary>
    /// 指数曲线缓动
    /// </summary>
    public class Expo
    {
        /// <summary>
        /// 异步调用缓动方法
        /// </summary>
        /// <param name="context">需要更新UI的线程上下文</param>
        /// <param name="currentTime">初始缓动步数</param>
        /// <param name="beginPosition">初始位置</param>
        /// <param name="variablePosition">位置变化量</param>
        /// <param name="duration">目标缓动步数</param>
        /// <param name="sleepMillSecond">延迟时间，以毫秒为单位</param>
        /// <param name="animateEvent">缓动动画回调</param>
        public static void setOnExpoAnimateCallback(SynchronizationContext context, double currentTime, int beginPosition, int variablePosition, double duration, int sleepMillSecond, AnimateEvent animateEvent, AnimationType type)
        {
            new Thread(() =>
            {
                int posi = beginPosition;
                while (currentTime < duration)
                {
                    switch (type)
                    {
                        case AnimationType.EASE_IN:
                            posi = (int)easeIn(currentTime++, beginPosition, variablePosition, duration);
                            break;
                        case AnimationType.EASE_OUT:
                            posi = (int)easeOut(currentTime++, beginPosition, variablePosition, duration);
                            break;
                        case AnimationType.EASE_IN_OUT:
                            posi = (int)easeInOut(currentTime++, beginPosition, variablePosition, duration);
                            break;
                    }
                    context.Post((n) => {
                        animateEvent((int)n);
                    }, posi);
                    Thread.Sleep(sleepMillSecond);
                }
            }).Start();
        }

        /// <summary>
        /// 同步调用缓动方法，缓动前半段
        /// </summary>
        /// <param name="currentTime">初始缓动参数</param>
        /// <param name="beginPosition">起始位置</param>
        /// <param name="variablePosition">位置变化量</param>
        /// <param name="duration">目标缓动参数</param>
        /// <returns>返回单次缓动后的位置</returns>
        public static double easeIn(double currentTime, int beginPosition, int variablePosition, double duration)
        {
            return (currentTime == 0) ? beginPosition : variablePosition * (double)Math.Pow(2, 10 * (currentTime / duration - 1)) + beginPosition;
        }

        /// <summary>
        /// 同步调用缓动方法，缓动后半段
        /// </summary>
        /// <param name="currentTime">初始缓动参数</param>
        /// <param name="beginPosition">起始位置</param>
        /// <param name="variablePosition">位置变化量</param>
        /// <param name="duration">目标缓动参数</param>
        /// <returns>返回单次缓动后的位置</returns>
        public static double easeOut(double currentTime, int beginPosition, int variablePosition, double duration)
        {
            return (currentTime == duration) ? beginPosition + variablePosition : variablePosition * (-(double)Math.Pow(2, -10 * currentTime / duration) + 1) + beginPosition;
        }

        /// <summary>
        /// 同步调用缓动方法，缓动整个时间段
        /// </summary>
        /// <param name="currentTime">初始缓动参数</param>
        /// <param name="beginPosition">起始位置</param>
        /// <param name="variablePosition">位置变化量</param>
        /// <param name="duration">目标缓动参数</param>
        /// <returns>返回单次缓动后的位置</returns>
        public static double easeInOut(double currentTime, int beginPosition, int variablePosition, double duration)
        {
            if (currentTime == 0) return beginPosition;
            if (currentTime == duration) return beginPosition + variablePosition;
            if ((currentTime /= duration / 2) < 1) return variablePosition / 2 * (double)Math.Pow(2, 10 * (currentTime - 1)) + beginPosition;
            return variablePosition / 2 * (-(double)Math.Pow(2, -10 * --currentTime) + 2) + beginPosition;
        }
    }

    /// <summary>
    /// 圆形曲线缓动
    /// </summary>
    public class Circ
    {
        /// <summary>
        /// 异步调用缓动方法
        /// </summary>
        /// <param name="context">需要更新UI的线程上下文</param>
        /// <param name="currentTime">初始缓动步数</param>
        /// <param name="beginPosition">初始位置</param>
        /// <param name="variablePosition">位置变化量</param>
        /// <param name="duration">目标缓动步数</param>
        /// <param name="sleepMillSecond">延迟时间，以毫秒为单位</param>
        /// <param name="animateEvent">缓动动画回调</param>
        public static void setOnCircAnimateCallback(SynchronizationContext context, double currentTime, int beginPosition, int variablePosition, double duration, int sleepMillSecond, AnimateEvent animateEvent, AnimationType type)
        {
            new Thread(() =>
            {
                int posi = beginPosition;
                while (currentTime < duration)
                {
                    switch (type)
                    {
                        case AnimationType.EASE_IN:
                            posi = (int)easeIn(currentTime++, beginPosition, variablePosition, duration);
                            break;
                        case AnimationType.EASE_OUT:
                            posi = (int)easeOut(currentTime++, beginPosition, variablePosition, duration);
                            break;
                        case AnimationType.EASE_IN_OUT:
                            posi = (int)easeInOut(currentTime++, beginPosition, variablePosition, duration);
                            break;
                    }
                    context.Post((n) => {
                        animateEvent((int)n);
                    }, posi);
                    Thread.Sleep(sleepMillSecond);
                }
            }).Start();
        }

        /// <summary>
        /// 同步调用缓动方法，缓动前半段
        /// </summary>
        /// <param name="currentTime">初始缓动参数</param>
        /// <param name="beginPosition">起始位置</param>
        /// <param name="variablePosition">位置变化量</param>
        /// <param name="duration">目标缓动参数</param>
        /// <returns>返回单次缓动后的位置</returns>
        public static double easeIn(double currentTime, int beginPosition, int variablePosition, double duration)
        {
            return -variablePosition * ((double)Math.Sqrt(1 - (currentTime /= duration) * currentTime) - 1) + beginPosition;
        }

        /// <summary>
        /// 同步调用缓动方法，缓动后半段
        /// </summary>
        /// <param name="currentTime">初始缓动参数</param>
        /// <param name="beginPosition">起始位置</param>
        /// <param name="variablePosition">位置变化量</param>
        /// <param name="duration">目标缓动参数</param>
        /// <returns>返回单次缓动后的位置</returns>
        public static double easeOut(double currentTime, int beginPosition, int variablePosition, double duration)
        {
            return variablePosition * (double)Math.Sqrt(1 - (currentTime = currentTime / duration - 1) * currentTime) + beginPosition;
        }

        /// <summary>
        /// 同步调用缓动方法，缓动整个时间段
        /// </summary>
        /// <param name="currentTime">初始缓动参数</param>
        /// <param name="beginPosition">起始位置</param>
        /// <param name="variablePosition">位置变化量</param>
        /// <param name="duration">目标缓动参数</param>
        /// <returns>返回单次缓动后的位置</returns>
        public static double easeInOut(double currentTime, int beginPosition, int variablePosition, double duration)
        {
            if ((currentTime /= duration / 2) < 1) return -variablePosition / 2 * ((double)Math.Sqrt(1 - currentTime * currentTime) - 1) + beginPosition;
            return variablePosition / 2 * ((double)Math.Sqrt(1 - (currentTime -= 2) * currentTime) + 1) + beginPosition;
        }
    }

    /// <summary>
    /// 弹性曲线缓动
    /// </summary>
    public class Elastic
    {
        /// <summary>
        /// 异步调用缓动方法
        /// </summary>
        /// <param name="context">需要更新UI的线程上下文</param>
        /// <param name="currentTime">初始化缓动步数</param>
        /// <param name="beginPosition">初始化缓动位置</param>
        /// <param name="variablePosition">位置变化量</param>
        /// <param name="duration">目标缓动步数</param>
        /// <param name="amplitude">缓动振幅量</param>
        /// <param name="period">振幅时间</param>
        /// <param name="sleepMillSecond">缓动延迟时间</param>
        /// <param name="animateEvent">动画回调</param>
        /// <param name="type">动画类型</param>
        public static void setOnElasticAnimateCallback(SynchronizationContext context, double currentTime, int beginPosition, int variablePosition, double duration, int amplitude, double period, int sleepMillSecond, AnimateEvent animateEvent, AnimationType type)
        {
            new Thread(() =>
            {
                int posi = beginPosition;
                while (currentTime < duration)
                {
                    switch (type)
                    {
                        case AnimationType.EASE_IN:
                            posi = (int)easeIn(currentTime++, beginPosition, variablePosition, duration, amplitude, period);
                            break;
                        case AnimationType.EASE_OUT:
                            posi = (int)easeOut(currentTime++, beginPosition, variablePosition, duration, amplitude, period);
                            break;
                        case AnimationType.EASE_IN_OUT:
                            posi = (int)easeInOut(currentTime++, beginPosition, variablePosition, duration, amplitude, period);
                            break;
                    }
                    context.Post((n) => {
                        animateEvent((int)n);
                    }, posi);
                    Thread.Sleep(sleepMillSecond);
                }
            }).Start();
        }

        /// <summary>
        /// 同步调用缓动方法，缓动前半段
        /// </summary>
        /// <param name="currentTime">初始化缓动步数</param>
        /// <param name="beginPosition">初始化缓动位置</param>
        /// <param name="variablePosition">位置变化量</param>
        /// <param name="duration">目标缓动步数</param>
        /// <param name="amplitude">缓动振幅量</param>
        /// <param name="period">振幅时间</param>
        /// <returns>返回单次缓动后的位置</returns>
        public static double easeIn(double currentTime, int beginPosition, int variablePosition, double duration, int amplitude, double period)
        {
            if (currentTime == 0) return beginPosition; if ((currentTime /= duration) == 1) return beginPosition + variablePosition; if (period != 0) period = duration * .3;
            double amount;
            if (amplitude != 0 || amplitude < Math.Abs(variablePosition)) { amplitude = variablePosition; amount = period / 4; }
            else amount = period / (2 * Math.PI) * (double)Math.Asin(variablePosition / amplitude);
            return -(double)(amplitude * Math.Pow(2, 10 * (currentTime -= 1)) * Math.Sin((currentTime * duration - amount) * (2 * Math.PI) / period)) + beginPosition;
        }

        /// <summary>
        /// 同步调用缓动方法，缓动后半段
        /// </summary>
        /// <param name="currentTime">初始化缓动步数</param>
        /// <param name="beginPosition">初始化缓动位置</param>
        /// <param name="variablePosition">位置变化量</param>
        /// <param name="duration">目标缓动步数</param>
        /// <param name="amplitude">缓动振幅量</param>
        /// <param name="period">振幅时间</param>
        /// <returns>返回单次缓动后的位置</returns>
        public static double easeOut(double currentTime, int beginPosition, int variablePosition, double duration, int amplitude, double period)
        {
            if (currentTime == 0) return beginPosition; if ((currentTime /= duration) == 1) return beginPosition + variablePosition; if (period != 0) period = duration * .3;
            double amount;
            if (amplitude != 0 || amplitude < Math.Abs(variablePosition)) { amplitude = variablePosition; amount = period / 4; }
            else amount = period / (2 * Math.PI) * Math.Asin(variablePosition / amplitude);
            return (double)(amplitude * Math.Pow(2, -10 * currentTime) * Math.Sin((currentTime * duration - amount) * (2 * Math.PI) / period) + variablePosition + beginPosition);
        }

        /// <summary>
        /// 同步调用缓动方法，完整时间段
        /// </summary>
        /// <param name="currentTime">初始化缓动步数</param>
        /// <param name="beginPosition">初始化缓动位置</param>
        /// <param name="variablePosition">位置变化量</param>
        /// <param name="duration">目标缓动步数</param>
        /// <param name="amplitude">缓动振幅量</param>
        /// <param name="period">振幅时间</param>
        /// <returns>返回单次缓动后的位置</returns>
        public static double easeInOut(double currentTime, int beginPosition, int variablePosition, double duration, int amplitude, double period)
        {
            if (currentTime == 0) return beginPosition; if ((currentTime /= duration / 2) == 2) return beginPosition + variablePosition; if (period != 0) period = duration * (.3 * 1.5);
            double amount;
            if (amplitude != 0 || amplitude < Math.Abs(variablePosition)) { amplitude = variablePosition; amount = period / 4; }
            else amount = period / (2 * Math.PI) * Math.Asin(variablePosition / amplitude);
            if (currentTime < 1) return (double)(-.5 * (amplitude * Math.Pow(2, 10 * (currentTime -= 1)) * Math.Sin((currentTime * duration - amount) * (2 * Math.PI) / period)) + beginPosition);
            return (double)(amplitude * Math.Pow(2, -10 * (currentTime -= 1)) * Math.Sin((currentTime * duration - amount) * (2 * Math.PI) / period) * .5 + variablePosition + beginPosition);
        }
    }

    /// <summary>
    /// 弹性三次方缓动
    /// </summary>
    public class Back
    {
        /// <summary>
        /// 异步调用缓动方法
        /// </summary>
        /// <param name="context">需要更新UI的线程上下文</param>
        /// <param name="currentTime">初始化缓动步数</param>
        /// <param name="beginPosition">初始化缓动位置</param>
        /// <param name="variablePosition">位置变化量</param>
        /// <param name="duration">目标缓动步数</param>
        /// <param name="sleepMillSecond">缓动延迟时间</param>
        /// <param name="animateEvent">动画回调</param>
        /// <param name="type">动画类型</param>
        /// <param name="amount">反弹量</param>
        public static void setOnBackAnimateCallback(SynchronizationContext context, double currentTime, int beginPosition, int variablePosition, double duration, int sleepMillSecond, AnimateEvent animateEvent, AnimationType type, double amount = 1.70158)
        {
            new Thread(() =>
            {
                int posi = beginPosition;
                while (currentTime < duration)
                {
                    switch (type)
                    {
                        case AnimationType.EASE_IN:
                            posi = (int)easeIn(currentTime++, beginPosition, variablePosition, duration, amount);
                            break;
                        case AnimationType.EASE_OUT:
                            posi = (int)easeOut(currentTime++, beginPosition, variablePosition, duration, amount);
                            break;
                        case AnimationType.EASE_IN_OUT:
                            posi = (int)easeInOut(currentTime++, beginPosition, variablePosition, duration, amount);
                            break;
                    }
                    context.Post((n) => {
                        animateEvent((int)n);
                    }, posi);
                    Thread.Sleep(sleepMillSecond);
                }
            }).Start();
        }

        /// <summary>
        /// 同步调用缓动方法，缓动前半段
        /// </summary>
        /// <param name="currentTime">初始化缓动步数</param>
        /// <param name="beginPosition">初始化缓动位置</param>
        /// <param name="variablePosition">位置变化量</param>
        /// <param name="duration">目标缓动步数</param>
        /// <param name="amount">反弹量</param>
        /// <returns>返回单次缓动之后的位置</returns>
        public static double easeIn(double currentTime, int beginPosition, int variablePosition, double duration, double amount)
        {
            return (double)(variablePosition * (currentTime /= duration) * currentTime * ((amount + 1) * currentTime - amount) + beginPosition);
        }

        /// <summary>
        /// 同步调用缓动方法，缓动后半段
        /// </summary>
        /// <param name="currentTime">初始化缓动步数</param>
        /// <param name="beginPosition">初始化缓动位置</param>
        /// <param name="variablePosition">位置变化量</param>
        /// <param name="duration">目标缓动步数</param>
        /// <param name="amount">反弹量</param>
        /// <returns>返回单次缓动之后的位置</returns>
        public static double easeOut(double currentTime, int beginPosition, int variablePosition, double duration, double amount)
        {
            return (double)(variablePosition * ((currentTime = currentTime / duration - 1) * currentTime * ((amount + 1) * currentTime + amount) + 1) + beginPosition);
        }

        /// <summary>
        /// 同步调用缓动方法，完整时间段
        /// </summary>
        /// <param name="currentTime">初始化缓动步数</param>
        /// <param name="beginPosition">初始化缓动位置</param>
        /// <param name="variablePosition">位置变化量</param>
        /// <param name="duration">目标缓动步数</param>
        /// <param name="amount">反弹量</param>
        /// <returns>返回单次缓动之后的位置</returns>
        public static double easeInOut(double currentTime, int beginPosition, int variablePosition, double duration, double amount)
        {
            if ((currentTime /= duration / 2) < 1) return (double)(variablePosition / 2 * (currentTime * currentTime * (((amount *= (1.525)) + 1) * currentTime - amount)) + beginPosition);
            return (double)(variablePosition / 2 * ((currentTime -= 2) * currentTime * (((amount *= (1.525)) + 1) * currentTime + amount) + 2) + beginPosition);
        }
    }

    /// <summary>
    /// 弹性指数缓动
    /// </summary>
    public class Bounce
    {
        /// <summary>
        /// 异步调用缓动方法
        /// </summary>
        /// <param name="context">需要更新UI的线程上下文</param>
        /// <param name="currentTime">初始缓动步数</param>
        /// <param name="beginPosition">初始位置</param>
        /// <param name="variablePosition">位置变化量</param>
        /// <param name="duration">目标缓动步数</param>
        /// <param name="sleepMillSecond">延迟时间，以毫秒为单位</param>
        /// <param name="animateEvent">缓动动画回调</param>
        public static void setOnBounceAnimateCallback(SynchronizationContext context, double currentTime, int beginPosition, int variablePosition, double duration, int sleepMillSecond, AnimateEvent animateEvent, AnimationType type)
        {
            new Thread(() =>
            {
                int posi = beginPosition;
                while (currentTime < duration)
                {
                    switch (type)
                    {
                        case AnimationType.EASE_IN:
                            posi = (int)easeIn(currentTime++, beginPosition, variablePosition, duration);
                            break;
                        case AnimationType.EASE_OUT:
                            posi = (int)easeOut(currentTime++, beginPosition, variablePosition, duration);
                            break;
                        case AnimationType.EASE_IN_OUT:
                            posi = (int)easeInOut(currentTime++, beginPosition, variablePosition, duration);
                            break;
                    }
                    context.Post((n) => {
                        animateEvent((int)n);
                    }, posi);
                    Thread.Sleep(sleepMillSecond);
                }
            }).Start();
        }

        /// <summary>
        /// 同步调用缓动方法，缓动后半段
        /// </summary>
        /// <param name="currentTime">初始缓动参数</param>
        /// <param name="beginPosition">起始位置</param>
        /// <param name="variablePosition">位置变化量</param>
        /// <param name="duration">目标缓动参数</param>
        /// <returns>返回单次缓动后的位置</returns>
        public static double easeOut(double currentTime, int beginPosition, int variablePosition, double duration)
        {
            if ((currentTime /= duration) < (1 / 2.75))
            {
                return (double)(variablePosition * (7.5625 * currentTime * currentTime) + beginPosition);
            }
            else if (currentTime < (2 / 2.75))
            {
                return variablePosition * (7.5625 * (currentTime -= (1.5 / 2.75)) * currentTime + .75) + beginPosition;
            }
            else if (currentTime < (2.5 / 2.75))
            {
                return variablePosition * (7.5625 * (currentTime -= (2.25 / 2.75)) * currentTime + .9375) + beginPosition;
            }
            else
            {
                return variablePosition * (7.5625 * (currentTime -= (2.625 / 2.75)) * currentTime + .984375) + beginPosition;
            }
        }

        /// <summary>
        /// 同步调用缓动方法，缓动前半段
        /// </summary>
        /// <param name="currentTime">初始缓动参数</param>
        /// <param name="beginPosition">起始位置</param>
        /// <param name="variablePosition">位置变化量</param>
        /// <param name="duration">目标缓动参数</param>
        /// <returns>返回单次缓动后的位置</returns>
        public static double easeIn(double currentTime, int beginPosition, int variablePosition, double duration)
        {
            return variablePosition - easeOut(duration - currentTime, 0, variablePosition, duration) + beginPosition;
        }

        /// <summary>
        /// 同步调用缓动方法，缓动整个时间段
        /// </summary>
        /// <param name="currentTime">初始缓动参数</param>
        /// <param name="beginPosition">起始位置</param>
        /// <param name="variablePosition">位置变化量</param>
        /// <param name="duration">目标缓动参数</param>
        /// <returns>返回单次缓动后的位置</returns>
        public static double easeInOut(double currentTime, int beginPosition, int variablePosition, double duration)
        {
            if (currentTime < duration / 2) return easeIn(currentTime * 2, 0, variablePosition, duration) * .5 + beginPosition;
            else return easeOut(currentTime * 2 - duration, 0, variablePosition, duration) * .5 + variablePosition * .5 + beginPosition;
        }
    }
}
