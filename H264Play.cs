using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace AlarmMapClient
{
    public class H264Play
    {
        //打开播放文件
        [DllImport("H264Play.dll")]
        public static extern bool H264_PLAY_OpenFile(Int32 nPort, String sFileName);
        //开始播放
        [DllImport("H264Play.dll")]
        public static extern bool H264_PLAY_Play(Int32 nPort, IntPtr hWnd);
        //停止播放
        [DllImport("H264Play.dll")]
        public static extern bool H264_PLAY_Stop(Int32 nPort);
        //播放暂停/恢复
        [DllImport("H264Play.dll")]
        public static extern bool H264_PLAY_Pause(Int32 nPort, bool bPause);
        //加快播放速度
        [DllImport("H264Play.dll")]
        public static extern bool H264_PLAY_Fast(Int32 nPort);
        //减慢播放速度
        [DllImport("H264Play.dll")]
        public static extern bool H264_PLAY_Slow(Int32 nPort);
        //关闭播放文件
        [DllImport("H264Play.dll")]
        public static extern bool H264_PLAY_CloseFile(Int32 nPort);
        //输入视频数据
        //[DllImport("H264Play.dll")]
        //public static extern bool H264_PLAY_InputVideoData(Int32 nPort);
        //回调函数
        public delegate void AudioCaptureCallBack(UInt16 pDataBuffer, UInt32 DataLength, Int32 nUser);
        //打开音频采集功能
        [DllImport("H264Play.dll")]
        public static extern bool H264_PLAY_StartAudioCapture(AudioCaptureCallBack pProc, Int32 nBitsPerSample, Int32 nSamplesPerSecond, Int32 nLength);
    }
}
